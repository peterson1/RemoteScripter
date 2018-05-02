using System.Windows.Controls;
using System.Windows.Media;

namespace CommonTools.Lib45.UIExtensions
{
    public static class ListScrollerExtensions
    {
        public static void ScrollToEndOnChange(this DataGrid dg)
        {
            if (dg.Items.Count == 0) return;
            if (VisualTreeHelper.GetChild(dg, 0) is Decorator border)
            {
                var scroll = border.Child as ScrollViewer;
                if (scroll != null) scroll.ScrollToEnd();
            }
        }
    }
}
