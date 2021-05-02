using System;
using System.Collections.Generic;
using DiscoveryTest.Forms.ViewModels;
using DryIoc;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.Services
{
    public class ViewLocator : IViewLocator
    {
        private readonly Dictionary<Type, Type> registrations = new Dictionary<Type, Type>();

        public void Register<TViewModel, TView>()
            where TViewModel : ViewModel
            where TView : Page
        {
            registrations[typeof(TViewModel)] = typeof(TView);
        }

        public Page CreateView<TViewModel>(IContainer dependencies)
            where TViewModel : ViewModel
        {
            if (!registrations.TryGetValue(typeof(TViewModel), out var pageType))
                return null;

            var pageCtor = pageType.GetConstructorOrNull();
            var page = pageCtor?.Invoke(new object[0]) as Page;

            var viewModelCtor = typeof(TViewModel).GetConstructorOrNull(typeof(IContainer));
            var viewModel = viewModelCtor?.Invoke(new object[] {dependencies});

            if (page != null && viewModel != null)
                page.BindingContext = viewModel;

            return page;
        }
        
        public Page CreateView(ViewModel viewModel)
        {
            if (!registrations.TryGetValue(viewModel.GetType(), out var pageType))
                return null;

            var pageCtor = pageType.GetConstructorOrNull();
            var page = pageCtor?.Invoke(new object[0]) as Page;

            if (page != null) page.BindingContext = viewModel;

            return page;
        }
    }
}