using System;
using System.Globalization;
using System.Windows.Data;

namespace CommonTools.Lib45.ValueConverters
{
    public class FirstLetterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((string)value)?.Substring(0, 1);


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
