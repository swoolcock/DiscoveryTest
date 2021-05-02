using DiscoveryTest.Forms.ViewModels;
using DryIoc;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.Services
{
    /// <summary>
    /// Provides a way of registering view/viewmodel pairs and instantiating <see cref="Page"/>s purely by view model.
    /// </summary>
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