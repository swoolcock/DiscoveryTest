using DiscoveryTest.Forms.ViewModels;

namespace DiscoveryTest.Forms.Views
{
    public partial class SearchPage
    {
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new SearchPageViewModel(Navigation);
        }
    }
}