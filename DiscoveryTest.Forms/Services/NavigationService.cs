using System.Threading.Tasks;
using DiscoveryTest.Forms.ViewModels;
using DryIoc;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IViewLocator viewLocator;
        private readonly INavigation navigation;
        
        public NavigationService(IViewLocator viewLocator, INavigation navigation)
        {
            this.viewLocator = viewLocator;
            this.navigation = navigation;
        }

        public async Task PushAsync<TViewModel>(IContainer dependencies) where TViewModel : ViewModel
        {
            var page = viewLocator.CreateView<TViewModel>(dependencies);
            await navigation.PushAsync(page);
        }
        
        public async Task PushAsync(ViewModel viewModel)
        {
            var page = viewLocator.CreateView(viewModel);
            await navigation.PushAsync(page);
        }

        public async Task<ViewModel> PopAsync()
        {
            var page = await navigation.PopAsync();
            return page?.BindingContext as ViewModel;
        }

        public async Task PopToRootAsync()
        {
            await navigation.PopToRootAsync();
        }
    }
}