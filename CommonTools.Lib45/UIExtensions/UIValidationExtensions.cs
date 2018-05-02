using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CommonTools.Lib45.UIExtensions
{
    public static class UIValidationExtensions
    {
        //https://stackoverflow.com/a/4650392/3973863
        public static bool AllFieldsValid(this DependencyObject obj)
            => !Validation.GetHasError(obj)
            && LogicalTreeHelper.GetChildren(obj)
                                .OfType<DependencyObject>()
                                .All(AllFieldsValid);
    }
}
