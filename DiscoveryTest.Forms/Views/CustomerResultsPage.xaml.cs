using DiscoveryTest.Forms.ViewModels;

namespace DiscoveryTest.Forms.Views
{
    public partial class CustomerResultsPage
    {
        public CustomerResultsPage(string parkCode, string arriving)
        {
            InitializeComponent();
            BindingContext = new CustomerResultsViewModel(Navigation, parkCode, arriving);
        }
    }
}