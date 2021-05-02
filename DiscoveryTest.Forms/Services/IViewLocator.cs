using DiscoveryTest.Forms.ViewModels;
using DryIoc;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.Services
{
    public interface IViewLocator
    {
        public void Register<TViewModel, TView>()
            where TViewModel : ViewModel
            where TView : Page;

        public Page CreateView(ViewModel viewModel);
        
        public Page CreateView<TViewModel>(IContainer dependencies)
            where TViewModel : ViewModel;
    }
}