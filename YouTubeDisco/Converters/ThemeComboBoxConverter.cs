using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace YouTubeDisco.Converters
{
    public class ThemeComboBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch (value)
            {
                case ElementTheme.Light:
                    return 0;
                case ElementTheme.Dark:
                    return 1;
                default:
                    return 2;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            switch (value)
            {
                case 0:
                    return ElementTheme.Light;
                case 1:
                    return ElementTheme.Dark;
                default:
                    return ElementTheme.Default;
            }
        }
    }
}