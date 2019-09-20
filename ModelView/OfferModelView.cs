using esoft.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace esoft.ModelView
{
    class OfferModelView : INotifyPropertyChanged
    {
        //TODO: SelectedAgent, SelectedEstate, Updating
        private Offer selectedOffer;
        private Client selectedClientOffer;
        public Client SelectedClientOffer { get => selectedClientOffer; set { selectedClientOffer = value; OnPropertyChanged("SelectedClientOffer"); } }
        public Offer SelectedOffer { get => selectedOffer; set { selectedOffer = value; SelectedClientOffer = selectedOffer.Client; OnPropertyChanged("SelectedOffer"); } }

        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Agent> Agents { get; set; }
        public ObservableCollection<Offer> Offers { get; set; }
        public OfferModelView()
        {
            try
            {
                using(Context db = new Context())
                {
                    Offers = new ObservableCollection<Offer> { };
                    Clients = new ObservableCollection<Client> { };
                    Agents = new ObservableCollection<Agent> { };

                    var result = db.Offers.Include("Estate").Where(o => !o.isDeleted && !o.isCompleted);
                    foreach(var o in result)
                    {
                        Offers.Add(o);
                    }

                    var resultClients = db.Clients.Where(c => !c.isDeleted);

                    foreach (var c in resultClients)
                    {
                        Clients.Add(c);
                    }

                    var resultAgents = db.Agents.Where(c => !c.isDeleted);
                    foreach (var c in resultAgents)
                    {
                        Agents.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return;
            }
        }

        public RelayCommand AddCommand { get
            {
                return new RelayCommand(delegate (object parameter)
                {
                    var values = (object[])parameter;
                    Client client = (Client)values[0];
                    Agent agent = (Agent)values[1];
                    Estate estate = (Estate)values[2];
                    var price = (string)values[3];

                    Offer offer = new Offer { Client = client, Agent = agent, Estate = estate, Price = Convert.ToInt32(price) };
                    Model.Model.Create(offer);
                    Offers.Insert(0, offer);
                    SelectedOffer = Offers[0];
                }, (obj) => {
                    var values = (object[])obj;
                    Client client = (Client)values[0];
                    Agent agent = (Agent)values[1];
                    Estate estate = (Estate)values[2];
                    var price = (string)values[3];

                    return (client != null) && (agent != null)
                    && (estate != null) &&
                    (!String.IsNullOrWhiteSpace(price) && 
                    Model.Checkers.IsUInt(price));
                });
            } }
        public RelayCommand SaveCommand
        {
            get
            {
                return new RelayCommand(delegate (object parameter)
                {

                }, (obj) => false);
            }
        }
        public RelayCommand RemoveCommand
        {
            get
            {
                return new RelayCommand(delegate (object parameter)
                {
                    Offer offer = parameter as Offer;
                    Model.Model.Save(offer);
                    Offers.Remove(offer);
                }, (obj) => SelectedOffer != null);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
