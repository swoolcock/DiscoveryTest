using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        private int busySemaphore;

        public bool IsBusy => busySemaphore > 0;

        protected INavigation Navigation { get; }

        protected ViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) => PropertyChanged?.Invoke(this, args);
        
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        protected IDisposable MakeBusy()
        {
            busySemaphore++;
            return new InvokeOnDisposal(() => busySemaphore--);
        }

        private struct InvokeOnDisposal : IDisposable
        {
            private Action action;

            public InvokeOnDisposal(Action action)
            {
                this.action = action;
            }

            public void Dispose() => action?.Invoke();
        }
    }
}