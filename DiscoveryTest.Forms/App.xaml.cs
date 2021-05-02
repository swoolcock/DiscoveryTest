using DiscoveryTest.Core.Services;
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
            
            Dependencies.Register<IRestService, DiscoveryRestService>();
            
            MainPage = new NavigationPage(new SearchPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}