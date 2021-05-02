using System;
using System.Globalization;
using DiscoveryTest.Forms.Services;
using DryIoc;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.ViewModels
{
    public class SearchPageViewModel : ViewModel
    {
        private string parkCode = "";

        public string ParkCode
        {
            get => parkCode;
            set => SetProperty(ref parkCode, value ?? "");
        }

        private string arriving = "";

        public string Arriving
        {
            get => arriving;
            set => SetProperty(ref arriving, value ?? "");
        }

        private Command searchCommand;
        public Command Search => searchCommand ??= new Command(performSearch);

        private readonly INavigationService navigationService;

        public SearchPageViewModel(IContainer dependencies) : base(dependencies)
        {
            Title = "Customer Search";
            navigationService = Dependencies.Resolve<INavigationService>();
        }
        
        private async void performSearch()
        {
            if (IsBusy) return;
            using (MakeBusy())
            {
                // TODO: display error messages for validation
                if (string.IsNullOrWhiteSpace(ParkCode)) return;
                if (string.IsNullOrWhiteSpace(Arriving)) return;
                if (!DateTime.TryParseExact(Arriving, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)) return;

                await navigationService.PushAsync(new CustomerResultsPageViewModel(Dependencies, ParkCode.ToUpper(), Arriving));
            }
        }
    }
}