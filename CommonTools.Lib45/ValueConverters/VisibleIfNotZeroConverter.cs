using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CommonTools.Lib45.ValueConverters
{
    public class VisibleIfNotZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => IsZero(value) ? Visibility.Collapsed : Visibility.Visible;


        private bool IsZero(object value)
        {
            if (value == null) return true;
            if (!int.TryParse(value.ToString(), out int val)) return true;
            return val == 0;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
