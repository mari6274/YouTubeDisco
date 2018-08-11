using System;
using Windows.UI.Xaml.Data;

namespace YouTubeDisco.Converters
{
    public class NegationValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => !(bool) value;

        public object ConvertBack(object value, Type targetType, object parameter, string language) => !(bool)value;
    }
}