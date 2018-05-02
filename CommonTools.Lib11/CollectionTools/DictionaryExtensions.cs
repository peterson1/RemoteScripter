using CommonTools.Lib11.ExceptionTools;
using System;
using System.Collections.Generic;

namespace CommonTools.Lib11.CollectionTools
{
    public static class DictionaryExtensions
    {
        public static void IfFound<TKey, TVal>(
            this Dictionary<TKey, TVal> dict, 
            TKey key, 
            Action<TVal> action,
            bool errorIfMissing = false)
        {
            if (dict.TryGetValue(key, out TVal value))
                action(value);
            else if (errorIfMissing)
                throw Fault.BadData($"Dictionary ‹{typeof(TVal).Name}› does not contain key [{key}].");
        }


        //http://stackoverflow.com/a/2601501/3973863
        public static TValue GetOrDefault<TKey, TValue>
            (this IDictionary<TKey, TValue> dictionary,
             TKey key,
             TValue defaultValue = default(TValue))
        {
            TValue value;
            if (key == null) return defaultValue;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static TValue GetOrDefault<TKey, TValue>
            (this IDictionary<TKey, TValue> dictionary,
             TKey key,
             Func<TValue> defaultValueProvider)
        {
            TValue value;
            if (key == null) return defaultValueProvider();
            return dictionary.TryGetValue(key, out value) ? value
                 : defaultValueProvider();
        }
    }
}
