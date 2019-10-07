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
                if (selectedOffer != null)
                {
                    SelectedClientOffer = selectedOffer.Client;
                    SelectedAgentOffer = selectedOffer.Agent;
                    SelectedEstateOffer = selectedOffer.Estate;
                }
                OnPropertyChanged("SelectedOffer");
            }
        }
        
        public static ObservableCollection<Client> Clients { get; set; }
        public static ObservableCollection<Agent> Agents { get; set; }
        public static ObservableCollection<Estate> Estates { get; set; }
        public static ObservableCollection<Offer> Offers { get; set; }
        public OfferModelView()
        {
            Clients = new ObservableCollection<Client> { };
            Agents = new ObservableCollection<Agent> { };
            Estates = new ObservableCollection<Estate> { };
            Offers = new ObservableCollection<Offer> { };
            CreateCollection();
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

                    Offer offer = new Offer { ClientID = client.ID, AgentID = agent.ID, EstateID = estate.ID, Price = Convert.ToInt32(price) };
                    Model.Model.Create(offer);
                    Model.Model.UpdateCollections();
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
                    var values = (object[])parameter;
                    Client client = (Client)values[0];
                    Agent agent = (Agent)values[1];
                    Estate estate = (Estate)values[2];
                    var price = (string)values[3];

                    Offer offer = new Offer { ClientID = client.ID, AgentID = agent.ID, EstateID = estate.ID,
                        Client = client, Agent = agent, Estate = estate,
                        Price = Convert.ToInt32(price), ID = SelectedOffer.ID };
                    Model.Model.Save(offer);

                    Model.Model.UpdateCollections();
                }, (obj) => {
                    if(SelectedOffer != null)
                    {
                        var values = (object[])obj;
                        Client client = (Client)values[0];
                        Agent agent = (Agent)values[1];
                        Estate estate = (Estate)values[2];
                        var price = (string)values[3];

                        return (client != null) && (agent != null) && (estate != null)
                        && (Model.Checkers.IsUInt(price) && !String.IsNullOrWhiteSpace(price) );
                    }
                    else
                    {
                        return false;
                    }
                });
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
        public static void Update()
        {
            Clients.Clear();
            Agents.Clear();
            Estates.Clear();
            Offers.Clear();

            CreateCollection();
        }
        public static void CreateCollection()
        {
            using (Context db = new Context())
            {
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
                var result = db.Offers.Include("Client").Include("Agent").Include("Estate").Where(c => !c.isDeleted && !c.isCompleted);
                foreach (var c in result)
                {
                    Offers.Add(c);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
