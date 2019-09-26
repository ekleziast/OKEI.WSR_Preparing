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
    class DealModelView : INotifyPropertyChanged
    {
        private Deal selectedDeal;
        public Deal SelectedDeal
        {
            get => selectedDeal;
            set {
                selectedDeal = value;
                if(selectedDeal != null)
                {
                    SelectedDealOffer = selectedDeal.Offer;
                    SelectedDealDemand = selectedDeal.Demand;
                }
                OnPropertyChanged("SelectedDeal");
            }
        }
        private Demand selectedDealDemand;
        public Demand SelectedDealDemand
        {
            get => selectedDealDemand;
            set {
                selectedDealDemand = value;
                if (selectedDealDemand != null)
                {
                    OffersInDeal = GetFilteredOffers(SelectedDealDemand);
                }
                OnPropertyChanged("SelectedDealDemand");
            }
        }

        private Offer selectedDealOffer;
        public Offer SelectedDealOffer
        {
            get => selectedDealOffer;
            set { selectedDealOffer = value; OnPropertyChanged("SelectedDealOffer"); }
        }
        public ObservableCollection<Deal> Deals { get; set; }
        public ObservableCollection<Demand> DemandsInDeal { get; set; }
        public ObservableCollection<Offer> OffersInDeal { get; set; }

        public DealModelView()
        {
            using(Context db = new Context())
            {
                OffersInDeal = new ObservableCollection<Offer> { };
                DemandsInDeal = new ObservableCollection<Demand> { };
                Deals = new ObservableCollection<Deal> { };

                var resultDemands = db.Demands.Include("Client").Include("Agent").Include("DemandFilter").Where(o => !o.isCompleted && !o.isDeleted);
                foreach(var r in resultDemands)
                {
                    DemandsInDeal.Add(r);
                }
                var resultOffers = db.Offers.Include("Estate").Include("Client").Include("Agent").Where(o => !o.isCompleted && !o.isDeleted);
                foreach (var r in resultOffers)
                {
                    OffersInDeal.Add(r);
                }
                var result = db.Deals.Where(o => !o.isDeleted);
                foreach (var r in result)
                {
                    Deals.Add(r);
                }
            }
        }

        public static ObservableCollection<Deal> CreateCollection()
        {
            ObservableCollection<Deal> deals = new ObservableCollection<Deal> { };
            using (Context db = new Context())
            {
                var result = db.Deals.Include("Demand").Include("Offer").Where(o => !o.isDeleted);
                foreach(var r in result)
                {
                    deals.Add(r);
                }
            }
            return deals;
        }
        private ObservableCollection<Offer> GetFilteredOffers(Demand demand)
        {
            ObservableCollection<Offer> offers = new ObservableCollection<Offer> { };
            using(Context db = new Context())
            {
                var result = db.Offers.Include("Estate").Include("Client").Include("Agent").Where(o => !o.isCompleted && IsOfferMatchConditions(demand, o));
                foreach(var r in result)
                {
                    offers.Add(r);
                }
            }
            return offers;
        }

        private bool IsOfferMatchConditions(Demand demand, Offer offer)
        {
            bool result = true;

            if(offer.Estate.EstateTypeID != demand.EstateTypeID) { result = false; return result; }
            
            if (demand.DemandFilter.MinPrice != null) { result = offer.Price >= demand.DemandFilter.MinPrice; }
            if (demand.DemandFilter.MaxPrice != null) { result = result && (offer.Price <= demand.DemandFilter.MaxPrice); }

            if (offer.Estate.Area != null)
            {
                if (demand.DemandFilter.MinArea != null) { result = result && (offer.Estate.Area >= demand.DemandFilter.MinArea); }
                if (demand.DemandFilter.MaxArea != null) { result = result && (offer.Estate.Area <= demand.DemandFilter.MaxArea); }
            }
            int type = offer.Estate.EstateTypeID;
            using (Context db = new Context())
            {
                switch (type) {
                    case 0:
                        Apartment apartment = db.Apartments.Where(o => offer.EstateID == o.ID).FirstOrDefault();

                        if (apartment.Floor != null)
                        {
                            if (demand.DemandFilter.MinFloor != null) { result = result && (apartment.Floor >= demand.DemandFilter.MinFloor); }
                            if (demand.DemandFilter.MaxFloor != null) { result = result && (apartment.Floor <= demand.DemandFilter.MaxFloor); }
                        }
                        if(apartment.RoomsApartment != null)
                        {
                            if (demand.DemandFilter.MinRooms != null) { result = result && (apartment.RoomsApartment >= demand.DemandFilter.MinRooms); }
                            if (demand.DemandFilter.MaxRooms != null) { result = result && (apartment.RoomsApartment <= demand.DemandFilter.MaxRooms); }
                        }
                        break;
                    case 1:
                        House house = db.Houses.Where(o => offer.EstateID == o.ID).FirstOrDefault();

                        if(house.Floors != null)
                        {
                            if (demand.DemandFilter.MinFloors != null) { result = result && (house.Floors >= demand.DemandFilter.MinFloors); }
                            if (demand.DemandFilter.MaxFloors != null) { result = result && (house.Floors <= demand.DemandFilter.MaxFloors); }
                        }
                        if(house.RoomsHouse != null)
                        {
                            if (demand.DemandFilter.MinRooms != null) { result = result && (house.RoomsHouse >= demand.DemandFilter.MinRooms); }
                            if (demand.DemandFilter.MaxRooms != null) { result = result && (house.RoomsHouse <= demand.DemandFilter.MaxRooms); }
                        }
                        break;
                    case 2:
                        Land land = db.Lands.Where(o => offer.EstateID == o.ID).FirstOrDefault();
                        break;
                }
            }
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
