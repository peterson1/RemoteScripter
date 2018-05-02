using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace CommonTools.Lib45.ValueConverters
{
    public class SpanDaysConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return CantBe("NULL");

            if (!int.TryParse(value.ToString(), out int num))
                return CantBe(value);

            var sufx = num == 1 ? "day" : "days";
            return $"{num:N0} {sufx}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return CantBe("NULL");

            var trunc = value.ToString()
                             .Replace("days", "")
                             .Replace("day", "")
                             .Replace(",", "")
                             .Trim();

            if (!int.TryParse(trunc, out int num))
                return CantBe(value);

            return num;
        }


        //private InvalidCastException CantBe(object invalidValue)
        //    => new InvalidCastException($"Days count should NOT be {invalidValue}");
        private ValidationResult CantBe(object invalidValue)
            => new ValidationResult(false, $"Days count should NOT be {invalidValue}");
    }
}
