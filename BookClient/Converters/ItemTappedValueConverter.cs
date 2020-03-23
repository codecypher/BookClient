using System;
using System.Globalization;
using Xamarin.Forms;

namespace BookClient.Converters
{
    // XAML Attached Properties Tricks in Xamarin.Forms
    // http://michaelridland.com/xamarin/xaml-attached-properties-tricks-in-xamarin-forms/
    public class ItemTappedValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ItemTappedEventArgs e = (ItemTappedEventArgs)parameter;
            return e.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}