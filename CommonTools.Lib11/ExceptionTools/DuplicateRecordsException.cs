using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonTools.Lib11.ExceptionTools
{
    public class DuplicateRecordsException : Exception
    {
        private DuplicateRecordsException(string message) : base(message)
        {
        }

        public static DuplicateRecordsException For<T>(IEnumerable<T> matches, string field, object value)
            => new DuplicateRecordsException(
                $"{matches.Count()} ‹{typeof(T).Name}› records found whose “{field}” is [{value}].");
    }
}
