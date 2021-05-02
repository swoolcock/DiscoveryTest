using System;
using System.Globalization;
using DiscoveryTest.Forms.Services;
using DryIoc;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.ViewModels
{
    /// <summary>
    /// View model for SearchPage.
    /// </summary>
    public class SearchPageViewModel : ViewModel
    {
        private const string requiredFieldErrorMessage = "Required field.";
        private const string invalidDateFormatErrorMessage = "Invalid date format.";
        
        private string parkCode = "";

        public string ParkCode
        {
            get => parkCode;
            set
            {
                SetProperty(ref parkCode, value ?? "");
                ParkCodeErrorMessage = "";
            }
        }

        private string arriving = "";

        public string Arriving
        {
            get => arriving;
            set
            {
                SetProperty(ref arriving, value ?? "");
                ArrivingErrorMessage = "";
            }
        }

        private string parkCodeErrorMessage = "";

        public string ParkCodeErrorMessage
        {
            get => parkCodeErrorMessage;
            set => SetProperty(ref parkCodeErrorMessage, value ?? "");
        }
        
        private string arrivingErrorMessage = "";

        public string ArrivingErrorMessage
        {
            get => arrivingErrorMessage;
            set => SetProperty(ref arrivingErrorMessage, value ?? "");
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
                bool valid = true;
                if (string.IsNullOrWhiteSpace(ParkCode))
                {
                    ParkCodeErrorMessage = requiredFieldErrorMessage;
                    valid = false;
                }

                if (string.IsNullOrWhiteSpace(Arriving))
                {
                    ArrivingErrorMessage = requiredFieldErrorMessage;
                    valid = false;
                }
                else if (!DateTime.TryParseExact(Arriving, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out _))
                {
                    ArrivingErrorMessage = invalidDateFormatErrorMessage;
                    valid = false;
                }

                if (valid)
                    await navigationService.PushAsync(new CustomerResultsPageViewModel(Dependencies, ParkCode.ToUpper(), Arriving));
            }
        }
    }
}