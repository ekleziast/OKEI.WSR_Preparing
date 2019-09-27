using esoft.Entity;
using esoft.ModelView;
using System;
using System.Windows;
using System.Windows.Controls;

namespace esoft.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new
            {
                agent = new AgentModelView(),
                client = new ClientModelView(),
                estate = new EstateModelView(),
                offer = new OfferModelView(),
                demand = new DemandModelView(),
                deal = new DealModelView()
            };
        }

        private void DealDemandsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Demand demand = (Demand)((ComboBox)sender).SelectedItem;
            if (demand != null)
            {
                DealModelView.GetFilteredOffers(demand);
            }
        }
    }
}
