using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CommonTools.Lib11.DataStructures
{
    public class UIList<T> : ObservableCollection<T>
    {                                              
        public event EventHandler<T> ItemOpened    = delegate { };
        public event EventHandler<T> ItemDeleted   = delegate { };
        public event EventHandler    ItemsReplaced = delegate { };
        //public event EventHandler<T> DeleteCurrentConfirmed = delegate { };


        public UIList()
        {
        }

        public UIList(IEnumerable<T> rowItems)
        {
            SetItems(rowItems);
        }

        public UIList(IEnumerable<T> rowItems, T summaryRow, double summaryAmount)
        {
            SetItems       (rowItems);
            SetSummary     (summaryRow);
            SummaryAmount = summaryAmount;
        }


        public double  SummaryAmount  { get; set; }
        public decimal TotalSum       { get; set; }
        public T       CurrentItem    { get; set; }


        public ObservableCollection<T>  SummaryRows  { get; } = new ObservableCollection<T>();


        public void RaiseItemOpened(T item)
        {
            if (item != null)
                ItemOpened?.Invoke(this, item);
        }


        public void RaiseItemDeleted(T item)
        {
            if (item != null)
                ItemDeleted?.Invoke(this, item);
        }


        //public void AskToDeleteCurrent(string message, Action deleteAction, string caption = "Confirm to Delete")
        //    => Alert


        public void SetItems(IEnumerable<T> items)
        {
            this.Clear();

            foreach (var item in items)
                this.Add(item);

            ItemsReplaced?.Invoke(this, EventArgs.Empty);
        }


        public void RemoveAll(Predicate<T> match)
        {
            var list = this.ToList();
            list.RemoveAll(match);
            SetItems(list);
        }


        public void SetSummary(T summaryRow)
        {
            SummaryRows.Clear();
            SummaryRows.Add(summaryRow);
        }
    }
}
