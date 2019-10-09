using esoft.Entity;
using esoft.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace esoft.ModelView
{
    public class AgentModelView : INotifyPropertyChanged
    {
        private Agent selectedAgent;
        public static ObservableCollection<Agent> Agents { get; set; }

        public Agent SelectedAgent
        {
            get { return selectedAgent; }
            set
            {
                selectedAgent = value;
                OnPropertyChanged("SelectedAgent");
            }
        }

        public AgentModelView()
        {
            Agents = new ObservableCollection<Agent> { };
            CreateCollection();
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return new RelayCommand(delegate (object parameter)
                {
                    Agent agent = GetAgent(parameter);
                    agent.ID = SelectedAgent.ID;
                    Model.Model.Save(agent);

                    Model.Model.UpdateCollections();
                }, (obj) =>
                {
                    if (SelectedAgent != null)
                    {
                        return IsCorrect(obj);
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
                    Agent agent = parameter as Agent;
                    if (agent != null)
                    {
                        Model.Model.Remove(agent);
                        Agents.Remove(agent);
                    }
                }, (obj) => SelectedAgent != null ? !IsAgentInAction(SelectedAgent) : false);
            }
        }
        
        public RelayCommand AddCommand
        {
            get
            {
                return new RelayCommand(delegate (object parameter)
                {
                    Agent agent = GetAgent(parameter);
                    Model.Model.Create(agent);

                    Model.Model.UpdateCollections();
                }, (obj) => {
                    return IsCorrect(obj);
                });
            }
        }
        private bool IsCorrect(object obj)
        {
            var values = (object[])obj;
            var firstName = (string)values[0];
            var middleName = (string)values[1];
            var lastName = (string)values[2];
            var dealShare = (string)values[3];
            return !(String.IsNullOrWhiteSpace(firstName) || String.IsNullOrWhiteSpace(middleName)
                || String.IsNullOrWhiteSpace(lastName))
                && IsDealShare(dealShare);
        }
        private static Agent GetAgent(object parameter)
        {
            var values = (object[])parameter;
            var firstName = (string)values[0];
            var middleName = (string)values[1];
            var lastName = (string)values[2];
            var dealShare = String.IsNullOrWhiteSpace((string)values[3]) ? 45 : Convert.ToInt32(values[3]);

            Agent agent = new Agent { DealShare = dealShare, FirstName = firstName, MiddleName = middleName, LastName = lastName };
            return agent;
        }
        private bool IsDealShare(string dealShare)
        {
            int result;
            bool isInt = Int32.TryParse(dealShare, out result);
            return isInt ? !(result > 100 || result < 0) : false || String.IsNullOrWhiteSpace(dealShare);
        }
        public static bool IsAgentInAction(Agent agent)
        {
            bool result = false;
            using (Context db = new Context())
            {
                var offer = db.Offers.Where(o => o.AgentID == agent.ID && !o.isDeleted).Any();
                var demand = db.Demands.Where(o => o.AgentID == agent.ID && !o.isDeleted).Any();

                result = offer || demand;
            }
            return result;
        }
        public static void Update()
        {
            Agents.Clear();
            CreateCollection();
        }
        public static void CreateCollection()
        {
            using (Context db = new Context())
            {
                var result = db.Agents.Where(c => !c.isDeleted);
                foreach (var c in result)
                {
                    Agents.Add(c);
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
