using System;
using System.Collections.ObjectModel;

namespace CommonTools.Lib11.LoggingTools
{
    public interface ILogList
    {
        ObservableCollection<LogEntry> List { get; }

        void  Add  (string message);
        void  Add  (Exception exception);
    }
}
