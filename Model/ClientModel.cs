using esoft.Entity;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;

namespace esoft.Model
{
    public class ClientModel : INotifyPropertyChanged
    {
        private string phone;
        private string email;

        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
