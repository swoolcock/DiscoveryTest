using System;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.Converters
{
    /// <summary>
    /// Converter for linking the <see cref="ListView.ItemTapped"/> event to an <see cref="ICommand"/>.
    /// </summary>
    public class ItemTappedEventArgsToItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (value as ItemTappedEventArgs)?.Item;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}