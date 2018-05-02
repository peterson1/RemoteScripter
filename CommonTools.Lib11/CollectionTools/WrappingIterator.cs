using System.Collections.Generic;

namespace CommonTools.Lib11.CollectionTools
{
    //https://stackoverflow.com/a/10283828/3973863
    public sealed class WrappingIterator<T>
    {
        private IList<T> _list;
        private int      _index;

        public WrappingIterator(IList<T> list, int index) 
        {
            _list  = list;
            _index = index < 0 ? list.Count - 1 : index;
        }

        public WrappingIterator(IList<T> list)
            : this(list, -1)
        {
        }


        public T GetNext()
        {
            _index++;

            if (_index == _list.Count)
                _index = 0;

            return _list[_index];
        }


        public static WrappingIterator<T> CreateAt(IList<T> list, T value)
        {
            var index = list.IndexOf(value);
            return new WrappingIterator<T>(list, index);
        }
    }
}
