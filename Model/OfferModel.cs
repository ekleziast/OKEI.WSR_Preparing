using esoft.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace esoft.Model
{
    class OfferModel : INotifyPropertyChanged
    {
        private int price;
        private Client client;
        private Agent agent;
        private Estate estate;
        private bool isCompleted;

        public int Price { get => price; set { price = value; OnPropertyChanged("OfferPrice"); } }
        public Client Client { get => client; set { client = value; OnPropertyChanged("OfferClient"); } }
        public Agent Agent { get => agent; set { agent = value; OnPropertyChanged("OfferAgent"); } }
        public Estate Estate { get => estate; set { estate = value; OnPropertyChanged("OfferEstate"); } }
        public bool IsCompleted { get => isCompleted; set { isCompleted = value; OnPropertyChanged("OfferIsCompleted"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
