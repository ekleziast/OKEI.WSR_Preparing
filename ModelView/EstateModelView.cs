using esoft.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace esoft.ModelView
{
    public class EstateModelView : INotifyPropertyChanged
    {
        private string filterString = "";
        private int typeFilter = -1;
        public ObservableCollection<Estate> Estates { get; set; }
        public ObservableCollection<Estate> FilteredEstates { get; set; }
        
        private Estate selectedEstate;
        public Estate SelectedEstate
        {
            get { return selectedEstate; }
            set
            {
                selectedEstate = value;
                OnPropertyChanged("SelectedEstate");
            }
        }
        public EstateModelView()
        {
            try
            {
                using (Context db = new Context())
                {
                    Estates = new ObservableCollection<Estate> { };
                    FilteredEstates = new ObservableCollection<Estate> { };
                    var result = db.Estates.Where(c => !c.isDeleted);

                    foreach (var c in result)
                    {
                        Estates.Add(c);
                    }
                    AcceptFilter();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return;
            }
        }
        public RelayCommand AcceptFilterCommand
        {
            get
            {
                return new RelayCommand(delegate (object parameter)
                {
                    var values = (object[])parameter;
                    var estateType = Convert.ToInt32(values[0]);
                    var filter = (string)values[1];
                    AcceptFilter(filter, estateType - 1);
                });
            }
        }
        private void AcceptFilter(string filter = "", int estateType = -1)
        {
            IEnumerable<Estate> result = Estates;
            if (estateType >= 0)
            {
                result = Estates.Where(e =>
                {
                    string address = $"г. {e.City} ул. {e.Street} д. {e.HouseNumber} кв. {e.ApartmentNumber}".ToLower();
                    return address.Contains(filter.ToLower()) == true && e.EstateTypeID == estateType;
                });
            }
            else
            {
                result = Estates.Where(e =>
                {
                    string address = $"г. {e.City} ул. {e.Street} д. {e.HouseNumber} кв. {e.ApartmentNumber}".ToLower();
                    return address.Contains(filter.ToLower()) == true;
                });
            }
            FilteredEstates.Clear();
            foreach (var c in result)
            {
                FilteredEstates.Add(c);
            }
            filterString = filter;
            typeFilter = estateType;
        }
        public RelayCommand AddCommand
        {
            get
            {
                return new RelayCommand(delegate (object parameter)
                {
                    Estate estate = GetEstate(parameter);
                    
                    Model.Model.Create(estate);
                    Estates.Insert(0, estate);
                    SelectedEstate = Estates[0];

                    AcceptFilter(filterString, typeFilter);
                }, (obj) => {
                    var values = (object[])obj;
                    var latitude = (string)values[4];
                    var longitude = (string)values[5];
                    var area = (string)values[6];

                    return IsDouble(area) && (IsDouble(latitude, -80, 80) && IsDouble(longitude, -180, 180)) &&
                    (String.IsNullOrWhiteSpace(latitude) == String.IsNullOrWhiteSpace(longitude));
                });
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                return new RelayCommand(delegate (object parameter)
                {
                    Estate estate = GetEstate(parameter);
                    estate.ID = SelectedEstate.ID;
                    Model.Model.Save(estate);

                    Estates.Remove(SelectedEstate);
                    Estates.Insert(0, estate);
                    SelectedEstate = Estates[0];

                    AcceptFilter(filterString, typeFilter);
                }, (obj) => {
                    var values = (object[])obj;
                    if (SelectedEstate != null)
                    {
                        var latitude = (string)values[4];
                        var longitude = (string)values[5];
                        var area = (string)values[6];

                        return IsDouble(area) && (IsDouble(latitude, -80, 80) && IsDouble(longitude, -180, 180)) &&
                        (String.IsNullOrWhiteSpace(latitude) == String.IsNullOrWhiteSpace(longitude));
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
                    Estate estate = parameter as Estate;
                    Model.Model.Remove(estate);
                    Estates.Remove(estate);
                    AcceptFilter(filterString, typeFilter);
                }, (obj) => SelectedEstate != null);
            }
        }
        private Estate GetEstate(object parameter)
        {
            var values = (object[])parameter;
            var city = (string)values[0];
            var street = (string)values[1];
            var houseNumber = (string)values[2];
            var apartmentNumber = (string)values[3];
            double? latitude = null;
            double? longitude = null;
            double? area = null;
            if (String.IsNullOrWhiteSpace((string)values[4]) != true)
            {
                latitude = Convert.ToDouble(((string)values[4]).Replace('.', ','));
                longitude = Convert.ToDouble(((string)values[5]).Replace('.', ','));
            }
            if (String.IsNullOrWhiteSpace((string)values[6]) != true)
            {
                area = Convert.ToDouble(values[6]);
            }
            Estate estate = new Estate();
            var typeInd = Convert.ToInt32(values[7]);
            switch (typeInd)
            {
                case 0:
                    var floor = (string)values[8];
                    var roomsApartment = (string)values[9];
                    estate = new Apartment
                    {
                        City = city,
                        Street = street,
                        HouseNumber = houseNumber,
                        ApartmentNumber = apartmentNumber,
                        Latitude = latitude,
                        Longitude = longitude,
                        Area = area,
                        Floor = floor,
                        RoomsApartment = roomsApartment,
                        EstateTypeID = 0
                    };
                    break;
                case 1:
                    var floors = (string)values[10];
                    var roomsHouse = (string)values[11];
                    estate = new House
                    {
                        City = city,
                        Street = street,
                        HouseNumber = houseNumber,
                        ApartmentNumber = apartmentNumber,
                        Latitude = latitude,
                        Longitude = longitude,
                        Area = area,
                        Floors = floors,
                        RoomsHouse = roomsHouse,
                        EstateTypeID = 1
                    };
                    break;
                case 2:
                    estate = new Land
                    {
                        City = city,
                        Street = street,
                        HouseNumber = houseNumber,
                        ApartmentNumber = apartmentNumber,
                        Latitude = latitude,
                        Longitude = longitude,
                        Area = area,
                        EstateTypeID = 2
                    };
                    break;
            }

            return estate;
        }
        private bool IsDouble(string d)
        {
            double result;
            d = d.Replace('.', ',');
            bool isDouble = Double.TryParse(d, out result);
            return isDouble ? !(result < 0) : false || String.IsNullOrWhiteSpace(d);
        }
        private bool IsDouble(string d, int minLim, int maxLim)
        {
            double result;
            d = d.Replace('.', ',');
            bool isDouble = Double.TryParse(d, out result);
            return isDouble ? !(result < minLim || result > maxLim) : false || String.IsNullOrWhiteSpace(d);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private string ConvertType(string type)
        {
            string result = "";
            switch (type)
            {
                case "Все":
                    result = "";
                    break;
                case "Квартиры":
                    result = "Apartment";
                    break;
                case "Дома":
                    result = "House";
                    break;
                case "Земля":
                    result = "Land";
                    break;
                case "Apartment":
                    result = "Квартира";
                    break;
                case "House":
                    result = "Дом";
                    break;
                case "Land":
                    result = "Земля";
                    break;
            }
            return result;
        }
        private int ConvertTypeToInt(string type)
        {
            int result = 0;
            switch (type)
            {
                case "Apartment":
                    result = 0;
                    break;
                case "House":
                    result = 1;
                    break;
                case "Land":
                    result = 2;
                    break;
            }
            return result;
        }
    }
}
