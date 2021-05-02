using DiscoveryTest.Core.Model;
using DiscoveryTest.Core.Services;
using DiscoveryTest.Forms.Services;
using DryIoc;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.ViewModels
{
    public class SubmitResponsePageViewModel : ViewModel
    {
        private const string requiredFieldErrorMessage = "Required field.";
        
        public CustomerDTO Customer { get; }

        private string emailAddress = "";

        public string EmailAddress
        {
            get => emailAddress;
            set
            {
                SetProperty(ref emailAddress, value ?? "");
                EmailAddressErrorMessage = "";
            }
        }

        private string emailAddressErrorMessage = "";

        public string EmailAddressErrorMessage
        {
            get => emailAddressErrorMessage;
            set => SetProperty(ref emailAddressErrorMessage, value ?? "");
        }

        private Command submitCommand;
        public Command Submit => submitCommand ??= new Command(performSubmit);

        private readonly IRestService restService;
        private readonly INavigationService navigationService;
        
        public SubmitResponsePageViewModel(IContainer dependencies, CustomerDTO customer) : base(dependencies)
        {
            Customer = customer;
            
            Title = $"Submit to: {Customer.GuestName}";
        
            restService = Dependencies.Resolve<IRestService>();
            navigationService = Dependencies.Resolve<INavigationService>();
        }
        
        private async void performSubmit()
        {
            if (IsBusy) return;
            using (MakeBusy())
            {
                if (string.IsNullOrWhiteSpace(EmailAddress))
                {
                    EmailAddressErrorMessage = requiredFieldErrorMessage;
                    return;
                }
                
                var response = await restService.PostResponseAsync(Customer.ReservationId, EmailAddress);
                if (response != null && response.TryGetValue("detail", out var detail))
                {
                    EmailAddressErrorMessage = detail;
                    return;
                }
                
                await navigationService.PopAsync();
            }
        }
    }
}