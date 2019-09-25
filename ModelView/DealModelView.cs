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
                    SelectedOfferDeal = selectedDeal.Offer;
                    SelectedDemandDeal = selectedDeal.Demand;
                }
                OnPropertyChanged("SelectedDeal");
            }
        }
        private Offer selectedOfferDeal;
        public Offer SelectedOfferDeal
        {
            get => selectedOfferDeal;
            set { selectedOfferDeal = value; OnPropertyChanged("SelectedOfferDeal"); }
        }
        private Demand selectedDemandDeal;
        public Demand SelectedDemandDeal
        {
            get => selectedDemandDeal;
            set { selectedDemandDeal = value; OnPropertyChanged("SelectedDemandDeal"); }
        }
        public ObservableCollection<Deal> Deals { get; set; }
        public ObservableCollection<Offer> OffersInDeal { get; set; }
        public ObservableCollection<Demand> DemandsInDeal { get; set; }

        public DealModelView()
        {
            Deals = CreateCollection();
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
                var result = db.Offers.Where(o => !o.isCompleted != true && IsOfferMatchConditions(demand, o));
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
                        if(demand.DemandFilter.MinFloor != null) { result = result & (apartment.Floor >= demand.DemandFilter.MinFloor); }
                        if (demand.DemandFilter.MaxFloor != null) { result = result & (apartment.Floor <= demand.DemandFilter.MaxFloor); }
                        break;
                    case 1:
                        House house = db.Houses.Where(o => offer.EstateID == o.ID).FirstOrDefault();
                        break;
                    case 2:
                        Land land = db.Lands.Where(o => offer.EstateID == o.ID).FirstOrDefault();
                        break;
            }
            }
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
