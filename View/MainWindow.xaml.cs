using esoft.Entity;
using esoft.ModelView;
using System.Windows;

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
            DataContext = new {
                agent = new AgentModelView(), client = new ClientModelView(),
                estate = new EstateModelView(),
                offer = new OfferModelView(), demand = new DemandModelView()};
        }
    }
}
