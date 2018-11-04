using System;
using Windows.UI.Xaml.Data;

namespace YouTubeDisco.Converters
{
    public class DownloadTaskStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => (int) value == (int) parameter;

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotSupportedException();
    }
}