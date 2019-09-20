using esoft.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace esoft.Model
{
    class EstateModel : INotifyPropertyChanged
    {
        private int estateTypeID;

        private string city;
        private string street;
        private string houseNumber;
        private string apartmentNumber;
        private double area;

        private double latitude;
        private double longitude;
        
        public string City { get => city; set { city = value; OnPropertyChanged("City");} }
        public string Street { get => street; set { street = value; OnPropertyChanged("Street"); } }
        public string Number { get => houseNumber; set { houseNumber = value; OnPropertyChanged("HouseNumber"); } }
        public string Apartment { get => apartmentNumber; set { apartmentNumber = value; OnPropertyChanged("ApartmentNumber"); } }
        public double Area { get => area; set { area = value; OnPropertyChanged("Area"); } }

        public double Latitude { get => latitude; set { latitude = value; OnPropertyChanged("Latitude"); } }
        public double Longitude { get => longitude; set { longitude = value; OnPropertyChanged("Longitude"); } }

        public int EstateTypeID { get => estateTypeID; set { estateTypeID = value; OnPropertyChanged("EstateTypeID"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
