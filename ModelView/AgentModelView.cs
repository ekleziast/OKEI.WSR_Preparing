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
        public ObservableCollection<Agent> Agents { get; set; }

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
            try
            {
                using (Context db = new Context())
                {
                    Agents = new ObservableCollection<Agent> { };
                    var result = db.Agents.Where(c => !c.isDeleted);

                    foreach (var a in result)
                    {
                        Agents.Add(a);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return;
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return new RelayCommand(delegate (object parameter)
                {
                    var values = (object[])parameter;
                    var firstName = (string)values[0];
                    var middleName = (string)values[1];
                    var lastName = (string)values[2];
                    var dealShare = String.IsNullOrEmpty((string)values[3]) ? 45 : Convert.ToInt32(values[3]);

                    Agent agent = new Agent { DealShare = dealShare, FirstName = firstName, MiddleName = middleName, LastName = lastName, ID = SelectedAgent.ID };
                    Model.Model.Save(agent);

                    Agents.Remove(SelectedAgent);
                    Agents.Insert(0, agent);
                    SelectedAgent = Agents[0];
                }, (obj) =>
                {
                    if (SelectedAgent != null)
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
                }, (obj) => SelectedAgent != null);
            }
        }
        
        public RelayCommand AddCommand
        {
            get
            {
                return new RelayCommand(delegate (object parameter)
                {
                    var values = (object[])parameter;
                    var firstName = (string)values[0];
                    var middleName = (string)values[1];
                    var lastName = (string)values[2];
                    var dealShare = String.IsNullOrWhiteSpace((string)values[3]) ? 45 : Convert.ToInt32(values[3]);
                    
                    Agent agent = new Agent { DealShare=dealShare, FirstName = firstName, MiddleName = middleName, LastName = lastName };
                    Model.Model.Create(agent);

                    Agents.Insert(0, agent);
                    SelectedAgent = Agents[0];
                }, (obj) => {
                    var values = (object[])obj;
                    var firstName = (string)values[0];
                    var middleName = (string)values[1];
                    var lastName = (string)values[2];
                    var dealShare = (string)values[3];
                    return !(String.IsNullOrWhiteSpace(firstName) || String.IsNullOrWhiteSpace(middleName)
                        || String.IsNullOrWhiteSpace(lastName))
                        && IsDealShare(dealShare);
                });
            }
        }
        private bool IsDealShare(string dealShare)
        {
            int result;
            bool isInt = Int32.TryParse(dealShare, out result);
            return isInt ? !(result > 100 || result < 0) : false || String.IsNullOrWhiteSpace(dealShare);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
