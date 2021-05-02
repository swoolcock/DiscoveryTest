using System;
using System.Globalization;
using Xamarin.Forms;

namespace DiscoveryTest.Forms.Converters
{
    public class ItemTappedEventArgsToItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (value as ItemTappedEventArgs)?.Item;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}