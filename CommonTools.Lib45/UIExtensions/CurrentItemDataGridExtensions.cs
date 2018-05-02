using CommonTools.Lib11.DataStructures;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommonTools.Lib45.UIExtensions
{
    public static class CurrentItemDataGridExtensions
    {
        public static void EnableOpenCurrent<T>(this DataGrid dg)
        {
            Action act = () => RaiseItemOpened<T>(dg);
            dg.MouseDoubleClick += (s, e) => act.Invoke();
            dg.PreviewKeyDown   += (s, e) =>
            {
                if (e.Key == Key.Enter) act.Invoke();
            };
        }


        private static void RaiseItemOpened<T>(DataGrid dg)
        {
            if (dg.SelectedIndex == -1) return;
            T item;
            try   { item = (T)dg.SelectedItem; }
            catch (InvalidCastException) { return; }
            if (item == null) return;

            var vm = dg.DataContext as UIList<T>;
            vm?.RaiseItemOpened(item);
        }
    }
}
