using System;
using System.Windows;
using System.Windows.Controls;

namespace CommonTools.Lib45.PrintTools
{
    public static class DataGridPrintExtensions
    {
        const double PPI = 96;

        public static bool AskToPrint(this DataGrid grid,
            string headerLeftText, string headerCenterText = null,
            string headerRightText = null, string footerCenterText = null,
            string printJobDesc = "Print Document")
        {
            var dlg = new PrintDialog();
            dlg.UserPageRangeEnabled = true;
            if (dlg.ShowDialog() == false) return false;

            var uri = new Uri("pack://application:,,,/CommonTools.Lib45;component/PrintTools/DataGridPaginatorResourceDictionary1.xaml");
            var rsrc = new ResourceDictionary { Source = uri };
            var dSrc = new MatelichDataGridPaginator(grid, dlg, headerLeftText, headerCenterText, headerRightText, footerCenterText, rsrc);


            if (dlg.PageRangeSelection == PageRangeSelection.AllPages)
                dlg.PrintDocument(dSrc, printJobDesc);
            else
            {
                var rnge = new PageRangeDocumentPaginator(dSrc, dlg.PageRange);
                dlg.PrintDocument(rnge, printJobDesc);
            }
            return true;
        }
    }
}
