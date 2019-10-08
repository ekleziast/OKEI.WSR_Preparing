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
        public static bool SelectionChanged = false;

        private double priceForBuyer = 0;
        public double PriceForBuyer
        {
            get => priceForBuyer;
            set
            {
                priceForBuyer = value;
                OnPropertyChanged("PriceForBuyer");
            }
        }
        private double priceForSeller = 0;
        public double PriceForSeller
        {
            get => priceForSeller;
            set
            {
                priceForSeller = value;
                OnPropertyChanged("PriceForSeller");
            }
        }
        private double taxesForBuyerAgent = 0;
        public double TaxesForBuyerAgent
        {
            get => taxesForBuyerAgent;
            set
            {
                taxesForBuyerAgent = value;
                OnPropertyChanged("TaxesForBuyerAgent");
            }
        }
        private double taxesForSellerAgent = 0;
        public double TaxesForSellerAgent
        {
            get => taxesForSellerAgent;
            set
            {
                taxesForSellerAgent = value;
                OnPropertyChanged("TaxesForSellerAgent");
            }
        }
        private double taxesForCompany = 0;
        public double TaxesForCompany
        {
            get => taxesForCompany;
            set
            {
                taxesForCompany = value;
                OnPropertyChanged("TaxesForCompany");
            }
        }

        private Deal selectedDeal;
        public Deal SelectedDeal
        {
            get => selectedDeal;
            set {
                selectedDeal = value;
                if(selectedDeal != null)
                {
                    GetFilteredOffers(SelectedDeal.Demand);
                    SelectionChanged = true;

                    SelectedDealOffer = selectedDeal.Offer;
                    SelectedDealDemand = selectedDeal.Demand;

                    if (selectedDealOffer != null && selectedDealDemand != null)
                    {
                        SetPrices(selectedDealOffer, selectedDealDemand);
                    }
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
                OnPropertyChanged("SelectedDealDemand");
            }
        }

        private Offer selectedDealOffer;
        public Offer SelectedDealOffer
        {
            get => selectedDealOffer;
            set
            {
                selectedDealOffer = value;
                OnPropertyChanged("SelectedDealOffer");
            }
        }
        public static ObservableCollection<Deal> Deals { get; set; }
        public static ObservableCollection<Demand> DemandsInDeal { get; set; }
        public static ObservableCollection<Offer> OffersInDeal { get; set; }
        public static ObservableCollection<Offer> FilteredOffers { get; set; }

        public DealModelView()
        {
            OffersInDeal = new ObservableCollection<Offer> { };
            FilteredOffers = new ObservableCollection<Offer> { };
            DemandsInDeal = new ObservableCollection<Demand> { };
            Deals = new ObservableCollection<Deal> { };
            CreateCollection();
        }
        public static void GetFilteredOffers(Demand demand)
        {
            FilteredOffers.Clear();
            var result = OffersInDeal.Where(o => !o.isCompleted && IsOfferMatchConditions(demand, o));
            foreach(var r in result)
            {
                FilteredOffers.Add(r);
            }
        }

        public RelayCommand AddCommand
        {
            get
            {
                return new RelayCommand(delegate (object parameter)
                {
                    Deal deal = GetDeal(parameter);
                    Model.Model.Create(deal);
                    Model.Model.UpdateCollections();
                }, (obj) => {
                    var values = (object[])obj;
                    Offer offer = (Offer)values[0];
                    Demand demand = (Demand)values[1];
                    return offer != null && demand != null;
                });
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return new RelayCommand(delegate (object parameter)
                {
                    Deal deal = GetDeal(parameter);
                    deal.ID = SelectedDeal.ID;
                    Model.Model.Save(deal);
                    Model.Model.UpdateCollections();
                }, (obj) => {
                    if (SelectedDeal != null)
                    {
                        var values = (object[])obj;
                        Offer offer = (Offer)values[0];
                        Demand demand = (Demand)values[1];

                        return offer != null && demand != null;
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
                    Deal deal = parameter as Deal;
                    Model.Model.Remove(deal);
                    Model.Model.UpdateCollections();
                }, (obj) => SelectedDeal != null);
            }
        }
        private Deal GetDeal(object parameter)
        {
            var values = (object[])parameter;
            Offer offer = (Offer) values[0];
            Demand demand = (Demand)values[1];

            Deal deal = new Deal { OfferID = offer.ID, DemandID = demand.ID };
            return deal;
        }

        private static bool IsOfferMatchConditions(Demand demand, Offer offer)
        {
            bool result = true;

            int type = offer.Estate.EstateTypeID;
            if (type != demand.EstateTypeID) { result = false; return result; }
            
            if (demand.DemandFilter.MinPrice != null) { result = result && (offer.Price >= demand.DemandFilter.MinPrice); }
            if (demand.DemandFilter.MaxPrice != null) { result = result && (offer.Price <= demand.DemandFilter.MaxPrice); }

            if (offer.Estate.Area != null)
            {
                if (demand.DemandFilter.MinArea != null) { result = result && (offer.Estate.Area >= demand.DemandFilter.MinArea); }
                if (demand.DemandFilter.MaxArea != null) { result = result && (offer.Estate.Area <= demand.DemandFilter.MaxArea); }
            }
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
                        if (apartment.RoomsApartment != null)
                        {
                            if (demand.DemandFilter.MinRooms != null) { result = result && (apartment.RoomsApartment >= demand.DemandFilter.MinRooms); }
                            if (demand.DemandFilter.MaxRooms != null) { result = result && (apartment.RoomsApartment <= demand.DemandFilter.MaxRooms); }
                        }
                        break;
                    case 1:
                        House house = db.Houses.Where(o => offer.EstateID == o.ID).FirstOrDefault();
                        if (house.Floors != null)
                        {
                            if (demand.DemandFilter.MinFloors != null) { result = result && (house.Floors >= demand.DemandFilter.MinFloors); }
                            if (demand.DemandFilter.MaxFloors != null) { result = result && (house.Floors <= demand.DemandFilter.MaxFloors); }
                        }
                        if (house.RoomsHouse != null)
                        {
                            if (demand.DemandFilter.MinRooms != null) { result = result && (house.RoomsHouse >= demand.DemandFilter.MinRooms); }
                            if (demand.DemandFilter.MaxRooms != null) { result = result && (house.RoomsHouse <= demand.DemandFilter.MaxRooms); }
                        }
                        break;
                    case 2:
                        break;
                }
            }
            return result;
        }

        private void SetPrices(Offer offer, Demand demand)
        {
            PriceForBuyer = GetPriceForBuyer(offer);
            PriceForSeller = GetPriceForSeller(offer);
            TaxesForBuyerAgent = GetTaxesForAgent(PriceForBuyer, demand.Agent.DealShare);
            TaxesForSellerAgent = GetTaxesForAgent(PriceForSeller, offer.Agent.DealShare);
            TaxesForCompany = GetTaxesForCompany(PriceForBuyer + PriceForSeller);
        }
        private double GetTaxesForAgent(double price, int dealShare)
        {
            double taxes = 0;
            taxes = price * dealShare * 0.01;
            return taxes;
        }
        private double GetTaxesForCompany(double price)
        {
            double taxes = 0;
            taxes = price - (TaxesForBuyerAgent + TaxesForSellerAgent);
            return taxes;
        }
        private double GetPriceForBuyer(Offer offer)
        {
            double price = 0;
            price = offer.Price * 0.03;
            return price;
        }
        private double GetPriceForSeller(Offer offer)
        {
            double price = 0;
            switch (offer.Estate.EstateTypeID)
            {
                case 0:
                    price = 36000 + 0.01 * offer.Price;
                    break;
                case 1:
                    price = 30000 + 0.01 * offer.Price;
                    break;
                case 2:
                    price = 30000 + 0.02 * offer.Price;
                    break;
            }
            return price;
        }

        public static void Update()
        {
            DemandsInDeal.Clear();
            OffersInDeal.Clear();
            FilteredOffers.Clear();
            Deals.Clear();

            CreateCollection();
        }
        public static void CreateCollection()
        {
            using (Context db = new Context())
            {
                var resultDemands = db.Demands.Include("Client").Include("Agent").Include("DemandFilter").Where(o => !o.isCompleted && !o.isDeleted);
                foreach (var r in resultDemands)
                {
                    DemandsInDeal.Add(r);
                }
                var resultOffers = db.Offers.Include("Estate").Include("Client").Include("Agent").Where(o => !o.isCompleted && !o.isDeleted);
                foreach (var r in resultOffers)
                {
                    OffersInDeal.Add(r);
                }
                var result = db.Deals.Include("Demand").Include("Offer").Where(o => !o.isDeleted);
                foreach (var r in result)
                {
                    Deals.Add(r);
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
