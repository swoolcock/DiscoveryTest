using System.Collections.ObjectModel;
using DiscoveryTest.Core.Model;
using DiscoveryTest.Core.Services;
using DiscoveryTest.Forms.Views;
using DryIoc;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.ViewModels
{
    public class CustomerResultsViewModel : ViewModel
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

        public string Title => $"{ParkCode}: {Arriving}";

        public string ParkCode { get; }
        public string Arriving { get; }

        private readonly IRestService restService;
        
        private Command customerTappedCommand;
        public Command CustomerTapped => customerTappedCommand ??= new Command(performCustomerTapped);
        
        private Command searchCommand;
        public Command Search => searchCommand ??= new Command(performSearch);
        
        public CustomerResultsViewModel(INavigation navigation, string parkCode, string arriving) : base(navigation)
        {
            ParkCode = parkCode;
            Arriving = arriving;

            restService = App.Dependencies.Resolve<IRestService>();
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
                await Navigation.PushAsync(new SubmitResponsePage(customer));
            }
        }
    }
}