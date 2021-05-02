using DiscoveryTest.Core.Model;
using DiscoveryTest.Core.Services;
using DryIoc;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.ViewModels
{
    public class SubmitResponsePageViewModel : ViewModel
    {
        public CustomerDTO Customer { get; }

        public string Title => $"Submit to: {Customer.GuestName}";

        private string emailAddress = "";

        public string EmailAddress
        {
            get => emailAddress;
            set => SetProperty(ref emailAddress, value ?? "");
        }

        private Command submitCommand;
        public Command Submit => submitCommand ??= new Command(performSubmit);

        private readonly IRestService restService;
        
        public SubmitResponsePageViewModel(INavigation navigation, CustomerDTO customer) : base(navigation)
        {
            Customer = customer;

            restService = App.Dependencies.Resolve<IRestService>();
        }
        
        private async void performSubmit()
        {
            if (IsBusy) return;
            using (MakeBusy())
            {
                // TODO: display error messages for validation
                if (string.IsNullOrWhiteSpace(EmailAddress)) return;
                await restService.PostResponseAsync(Customer.ReservationId, EmailAddress);
                await Navigation.PopAsync();
            }
        }
    }
}