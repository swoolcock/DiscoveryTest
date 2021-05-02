using System;
using System.Globalization;
using DiscoveryTest.Forms.Views;
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

        public SearchPageViewModel(INavigation navigation) : base(navigation)
        {
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

                // if (!Regex.IsMatch(Arriving, "^\\d{4}-\\d{2}-\\d{2}$")) return;
                await Navigation.PushAsync(new CustomerResultsPage(ParkCode.ToUpper(), Arriving));
            }
        }
    }
}