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
    class HouseModel : INotifyPropertyChanged
    {
        private string floors;
        private string roomsHouse;

        public string Floors { get => floors; set { floors = value; OnPropertyChanged("Floors"); } }
        public string RoomsHouse { get => roomsHouse; set { roomsHouse = value; OnPropertyChanged("RoomsHouse"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
