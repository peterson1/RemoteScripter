using CommonTools.Lib11.StringTools;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CommonTools.Lib45.ValueConverters
{
    public class UnderscoreToSpaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            var text = value.ToString();
            if (text.IsBlank()) return "";
            return text.Replace('_', ' ');
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
