using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CommonTools.Lib45.ValueConverters
{
    public class VisibleIfTrueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (bool)value ? Visibility.Visible : Visibility.Collapsed;


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
