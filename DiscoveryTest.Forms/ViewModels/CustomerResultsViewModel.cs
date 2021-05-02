using System;
using System.Collections.ObjectModel;
using DiscoveryTest.Core.Model;
using DiscoveryTest.Core.Services;
using DryIoc;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.ViewModels
{
    public class CustomerResultsViewModel : ViewModel
    {
        public ObservableCollection<CustomerDTO> Customers { get; } = new ObservableCollection<CustomerDTO>();

        public string ParkCode { get; }
        public string Arriving { get; }

        private readonly IRestService restService;
        
        private Command customerTappedCommand;
        public Command CustomerTapped => customerTappedCommand ??= new Command(performCustomerTapped);
        
        public CustomerResultsViewModel(INavigation navigation, string parkCode, string arriving) : base(navigation)
        {
            ParkCode = parkCode;
            Arriving = arriving;

            restService = App.Dependencies.Resolve<IRestService>();

            performSearch();
        }

        private async void performSearch()
        {
            var results = await restService.GetCustomersAsync(ParkCode, Arriving);
            Customers.Clear();
            foreach (var customer in results)
                Customers.Add(customer);
        }

        private async void performCustomerTapped(object data)
        {
            var customer = (CustomerDTO)data;
            Console.WriteLine(customer.Title);
        }
    }
}