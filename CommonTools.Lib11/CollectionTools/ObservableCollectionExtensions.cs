using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CommonTools.Lib11.CollectionTools
{
    public static class ObservableCollectionExtensions
    {
        public static void SetContents<T>(this ObservableCollection<T> colxn, IEnumerable<T> source)
        {
            colxn.Clear();
            foreach (var item in source)
                colxn.Add(item);
        }
    }
}
