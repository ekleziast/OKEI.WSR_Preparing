using esoft.Entity;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace esoft.ModelView
{
    class OfferModelView : INotifyPropertyChanged
    {
        //TODO: SelectedAgent, SelectedEstate, Updating
        private Offer selectedOffer;
        private Client selectedClientOffer;
        private Estate selectedEstateOffer;
        private Agent selectedAgentOffer;

        public Client SelectedClientOffer { get => selectedClientOffer; set { selectedClientOffer = value; OnPropertyChanged("SelectedClientOffer"); } }
        public Agent SelectedAgentOffer { get => selectedAgentOffer; set { selectedAgentOffer = value; OnPropertyChanged("SelectedAgentOffer"); } }
        public Estate SelectedEstateOffer { get => selectedEstateOffer; set { selectedEstateOffer = value; OnPropertyChanged("SelectedEstateOffer"); } }
        public Offer SelectedOffer { get => selectedOffer;
            set {
                selectedOffer = value;
                SelectedClientOffer = selectedOffer.Client;
                SelectedAgentOffer = selectedOffer.Agent;
                SelectedEstateOffer = selectedOffer.Estate;
                OnPropertyChanged("SelectedOffer");
            }
        }
        

        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Agent> Agents { get; set; }
        public ObservableCollection<Estate> Estates { get; set; }
        public ObservableCollection<Offer> Offers { get; set; }
        public OfferModelView()
        {
            try
            {
                using (Context db = new Context())
                {
                    Agents = new ObservableCollection<Agent> { };
                    Clients = new ObservableCollection<Client> { };
                    Estates = new ObservableCollection<Estate> { };
                    Offers = new ObservableCollection<Offer> { };

                    var resultEstates = db.Estates.Where(c => !c.isDeleted);
                    foreach (var c in resultEstates)
                    {
                        Estates.Add(c);
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

                    var result = db.Offers.Where(o => !o.isDeleted && !o.isCompleted);
                    foreach (var o in result)
                    {
                        Offers.Add(o);
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
        public static ObservableCollection<Offer> CreateCollection()
        {
            ObservableCollection<Offer> offers = new ObservableCollection<Offer> { };
            using (Context db = new Context())
            {
                offers = new ObservableCollection<Offer> { };
                var result = db.Offers.Where(c => !c.isDeleted && !c.isCompleted);
                foreach (var c in result)
                {
                    offers.Add(c);
                }
            }
            return offers;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
