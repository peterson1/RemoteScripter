using CommonTools.Lib11.StringTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CommonTools.Lib45.BaseViewModels
{
    public abstract class TextFilterBase<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private      EventHandler _textFilterChanged;
        public event EventHandler  TextFilterChanged
        {
            add    { _textFilterChanged -= value; _textFilterChanged += value; }
            remove { _textFilterChanged -= value; }
        }


        public TextFilterBase()
        {
            this.PropertyChanged += TextFilterBase_PropertyChanged;
        }


        protected abstract Dictionary<string, Func<T, string>> FilterProperties { get; }


        public void RemoveNonMatches(ref List<T> list)
        {
            foreach (var kvp in FilterProperties)
            {
                var propVal = GetType()?.GetProperty(kvp.Key)?
                                        .GetValue(this)?
                                        .ToString();
                RemoveNonMatches(ref list, propVal, kvp.Value);
            }
        }


        private void TextFilterBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (FilterProperties.Keys.Contains(e.PropertyName))
                _textFilterChanged?.Invoke(sender, EventArgs.Empty);
        }


        protected void RemoveNonMatches(ref List<T> list, string filterString, Func<T, string> propertyGetter)
        {
            if (filterString.IsBlank()) return;
            var findThis = filterString.ToLower();

            try
            {
                list.RemoveAll(x => !propertyGetter(x).ToLower().HasText(findThis));
            }
            catch { }
        }
    }
}
