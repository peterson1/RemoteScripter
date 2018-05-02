using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CommonTools.Lib45.ValueConverters
{
    public class DecimalProxyConverter : IValueConverter
    {
        private string user_string = null;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (user_string != null)
            {
                return user_string;
            }

            if (value == null) return null;
            //var number = (decimal)value;
            //return string.Format(CultureInfo.CurrentCulture, "{0}", number);
            return decimal.TryParse(value.ToString(), out decimal number)
                 ? string.Format(CultureInfo.CurrentCulture, "{0}", number)
                 : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s_value = value.ToString();
            decimal result = 0;

            if (!decimal.TryParse(s_value, System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.CurrentCulture, out result))
                return null;

            user_string = s_value;

            return result;
        }

        public static DependencyProperty GetUpdateOnLostFocus(DependencyObject obj)
        {
            return (DependencyProperty)obj.GetValue(UpdateOnLostFocusProperty);
        }

        public static void SetUpdateOnLostFocus(DependencyObject obj, DependencyProperty value)
        {
            obj.SetValue(UpdateOnLostFocusProperty, value);
        }

        public static readonly DependencyProperty UpdateOnLostFocusProperty =
            DependencyProperty.RegisterAttached("UpdateOnLostFocus", typeof(DependencyProperty),
            typeof(DecimalProxyConverter), new UIPropertyMetadata(null, UpdateOnLostFocusChanged));

        private static void UpdateOnLostFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;

            if (e.NewValue != null)
            {
                element.LostFocus += new RoutedEventHandler(element_LostFocus);
            }

            if (e.OldValue != null)
            {
                element.LostFocus -= new RoutedEventHandler(element_LostFocus);
            }
        }

        static void element_LostFocus(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            BindingExpression bindingExpression = element.GetBindingExpression(GetUpdateOnLostFocus(element));
            Binding parentBinding = bindingExpression.ParentBinding;
            var converter = parentBinding.Converter as DecimalProxyConverter;
            if (converter != null)
            {
                converter.user_string = null;
                bindingExpression.UpdateTarget();
            }
        }
    }
}
