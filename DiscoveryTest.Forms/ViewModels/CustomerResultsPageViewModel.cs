using System.Collections.ObjectModel;
using DiscoveryTest.Core.Model;
using DiscoveryTest.Core.Services;
using DiscoveryTest.Forms.Services;
using DryIoc;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.ViewModels
{
    /// <summary>
    /// View model for CustomerResultsPage.
    /// </summary>
    public class CustomerResultsPageViewModel : ViewModel
    {
        private const string defaultStatusText = "Loading...";
        private const string noResultsStatusText = "No results found.";
        
        private string statusText = defaultStatusText;

        public string StatusText
        {
            get => statusText;
            set => SetProperty(ref statusText, value ?? "");
        }
        
        public ObservableCollection<CustomerDTO> Customers { get; } = new ObservableCollection<CustomerDTO>();

        public string ParkCode { get; }
        public string Arriving { get; }

        private readonly IRestService restService;
        private readonly INavigationService navigationService;
        
        private Command customerTappedCommand;
        public Command CustomerTapped => customerTappedCommand ??= new Command(performCustomerTapped);
        
        private Command searchCommand;
        public Command Search => searchCommand ??= new Command(performSearch);
        
        public CustomerResultsPageViewModel(IContainer dependencies, string parkCode, string arriving) : base(dependencies)
        {
            ParkCode = parkCode;
            Arriving = arriving;
            
            Title = $"{ParkCode}: {Arriving}";

            restService = Dependencies.Resolve<IRestService>();
            navigationService = Dependencies.Resolve<INavigationService>();
        }

        private async void performSearch()
        {
            if (IsBusy) return;
            using (MakeBusy())
            {
                var results = await restService.GetCustomersAsync(ParkCode, Arriving);

                Customers.Clear();

                foreach (var customer in results)
                    Customers.Add(customer);
                
                StatusText = Customers.Count == 0 ? noResultsStatusText : "";
            }
        }

        private async void performCustomerTapped(object data)
        {
            if (IsBusy) return;
            using (MakeBusy())
            {
                var customer = (CustomerDTO)data;
                await navigationService.PushAsync(new SubmitResponsePageViewModel(Dependencies, customer));
            }
        }
    }
}