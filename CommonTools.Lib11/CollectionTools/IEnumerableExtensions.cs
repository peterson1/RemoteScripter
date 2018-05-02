using CommonTools.Lib11.ExceptionTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonTools.Lib11.CollectionTools
{
    public static class IEnumerableExtensions
    {
        public static T GetOne<T>(this IEnumerable<T> collection, Func<T, bool> predicate, string predicateDescription)
        {
            if (collection == null)
                throw Fault.BadData("collection == NULL");

            if (!collection.Any())
                throw Fault.BadData("Collection has no items");

            var matches = collection.Where(predicate);

            if (matches.Count() == 1)
                return matches.First();

            var typ = typeof(T).Name;

            if (matches.Count() == 0)
                throw Fault.BadData($"No ‹{typ}› found where [{predicate}].");
            else
                throw Fault.BadData($"Multiple ‹{typ}› found where [{predicate}].");
        }


        /// <summary>
        /// Immediately executes the given action on each element in the source sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence</typeparam>
        /// <param name="source">The sequence of elements</param>
        /// <param name="action">The action to execute on each element</param>

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (action == null) throw new ArgumentNullException(nameof(action));

            foreach (var element in source)
                action(element);
        }

        /// <summary>
        /// Immediately executes the given action on each element in the source sequence.
        /// Each element's index is used in the logic of the action.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence</typeparam>
        /// <param name="source">The sequence of elements</param>
        /// <param name="action">The action to execute on each element; the second parameter
        /// of the action represents the index of the source element.</param>

        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (action == null) throw new ArgumentNullException(nameof(action));

            var index = 0;
            foreach (var element in source)
                action(element, index++);
        }
    }
}
