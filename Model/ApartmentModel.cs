using esoft.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace esoft.ModelView
{
    public class ApartmentModel : INotifyPropertyChanged
    {
        private string floor;
        private string roomsApartment;

        public string Floor { get => floor; set { floor = value; OnPropertyChanged("Floor"); } }
        public string RoomsApartment { get => roomsApartment; set { roomsApartment = value; OnPropertyChanged("RoomsApartment"); } }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
