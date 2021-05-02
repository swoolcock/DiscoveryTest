using DiscoveryTest.Core.Model;
using DiscoveryTest.Forms.ViewModels;

namespace DiscoveryTest.Forms.Views
{
    public partial class SubmitResponsePage
    {
        public SubmitResponsePage(CustomerDTO customer)
        {
            InitializeComponent();
            BindingContext = new SubmitResponsePageViewModel(Navigation, customer);
        }
    }
}