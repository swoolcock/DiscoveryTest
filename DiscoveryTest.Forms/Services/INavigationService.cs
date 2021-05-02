using System.Threading.Tasks;
using DiscoveryTest.Forms.ViewModels;
using DryIoc;

namespace DiscoveryTest.Forms.Services
{
    public interface INavigationService
    {
        Task PushAsync<TViewModel>(IContainer dependencies) where TViewModel : ViewModel;
        Task PushAsync(ViewModel viewModel);
        Task<ViewModel> PopAsync();
        Task PopToRootAsync();
    }
}