using DiscoveryTest.Core.Services;
using DiscoveryTest.Forms.Services;
using DiscoveryTest.Forms.ViewModels;
using DiscoveryTest.Forms.Views;
using DryIoc;
using Xamarin.Forms;

namespace DiscoveryTest.Forms
{
    public partial class App
    {
        public static Container Dependencies { get; } = new Container();
        
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage();
            
            Dependencies.RegisterInstance(MainPage.Navigation);
            Dependencies.Register<IViewLocator, ViewLocator>(Reuse.Singleton);
            Dependencies.Register<INavigationService, NavigationService>();
            Dependencies.Register<IRestService, DiscoveryRestService>();

            var viewLocator = Dependencies.Resolve<IViewLocator>();
            viewLocator.Register<SearchPageViewModel, SearchPage>();
            viewLocator.Register<CustomerResultsPageViewModel, CustomerResultsPage>();
            viewLocator.Register<SubmitResponsePageViewModel, SubmitResponsePage>();

            var navigationService = Dependencies.Resolve<INavigationService>();
            navigationService.PushAsync<SearchPageViewModel>(Dependencies);
        }
    }
}