using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IContainer = DryIoc.IContainer;

namespace DiscoveryTest.Forms.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        private int busySemaphore;

        public bool IsBusy => busySemaphore > 0;
        public bool IsEnabled => !IsBusy;

        private string title = "";

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value ?? "");
        }
        
        protected IContainer Dependencies { get; }

        protected ViewModel(IContainer dependencies)
        {
            Dependencies = dependencies;
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
            RaisePropertyChanged(nameof(IsBusy));
            RaisePropertyChanged(nameof(IsEnabled));
            return new InvokeOnDisposal(() =>
            {
                busySemaphore--;
                RaisePropertyChanged(nameof(IsBusy));
                RaisePropertyChanged(nameof(IsEnabled));
            });
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