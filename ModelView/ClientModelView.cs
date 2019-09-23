using esoft.Entity;
using esoft.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace esoft.ModelView
{
    public class ClientModelView : INotifyPropertyChanged
    {
        
        private Client selectedClient;
        public ObservableCollection<Client> Clients { get; set; }

        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }

        public ClientModelView()
        {
            Clients = CreateCollection();
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
                    var email = (string)values[3];
                    var phone = (string)values[4];
                    
                    Client client = new Client { Phone = phone, Email = email, FirstName = firstName, MiddleName = middleName, LastName = lastName, ID = SelectedClient.ID };
                    
                    Model.Model.Save(client);

                    Clients.Remove(SelectedClient);
                    Clients.Insert(0, client);
                    SelectedClient = Clients[0];
                }, (obj) => 
                {
                    var values = (object[])obj;
                    if (SelectedClient != null)
                    {
                        var email = (string)values[3];
                        var phone = (string)values[4];
                        if (!String.IsNullOrEmpty((string)values[3]) && !String.IsNullOrEmpty((string)values[4]))
                        {
                            return (IsValidEmail(email) && IsValidPhone(phone));
                        }
                        return !String.IsNullOrEmpty(email) && IsValidEmail(email) || !String.IsNullOrEmpty(phone) && IsValidPhone(phone);
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
                    Client client = parameter as Client;
                    if(client != null)
                    {
                        Model.Model.Remove(client);
                        Clients.Remove(client);
                    }
                }, (obj) => SelectedClient != null);
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
                    var email = (string)values[3];
                    var phone = (string)values[4];
                    
                    Client client = new Client { Phone = phone, Email = email, FirstName = firstName, MiddleName = middleName, LastName = lastName };

                    Model.Model.Create(client);
                    Clients.Insert(0, client);
                    SelectedClient = Clients[0];
                }, (obj) => {
                    var values = (object[])obj;
                    var email = (string)values[3];
                    var phone = (string)values[4];

                    if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(phone))
                    {
                        return (IsValidEmail(email) && IsValidPhone(phone));
                    }
                    return !String.IsNullOrEmpty(email) && IsValidEmail(email) || !String.IsNullOrEmpty(phone) && IsValidPhone(phone);
                });
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            } catch {
                return false;
            }
        }
        private bool IsValidPhone(string phone)
        {
            return Regex.Match(phone, @"^(\+[0-9]{11})$").Success;
        }

        public static ObservableCollection<Client> CreateCollection()
        {
            ObservableCollection<Client> clients = new ObservableCollection<Client> { };
            using (Context db = new Context())
            {
                clients = new ObservableCollection<Client> { };
                var result = db.Clients.Where(c => !c.isDeleted);
                foreach (var c in result)
                {
                    clients.Add(c);
                }
            }
            return clients;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
