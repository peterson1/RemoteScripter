using System;

namespace CommonTools.Lib11.ExceptionTools
{
    public class IntrusionAttemptException : Exception
    {
        public IntrusionAttemptException() { }

        public IntrusionAttemptException(string message)
            : base(message) { }

        public IntrusionAttemptException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
