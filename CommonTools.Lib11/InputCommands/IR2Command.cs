using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommonTools.Lib11.InputCommands
{
    public interface IR2Command : ICommand
    {
        string    CurrentLabel      { get; set; }
        bool      IsBusy            { get; }
        bool      IsCheckable       { get; set; }
        bool      IsChecked         { get; set; }
        bool      OverrideEnabled   { get; set; }
        bool      DisableWhenDone   { get; set; }
        bool      LastExecutedOK    { get; }
        DateTime  LastExecuteStart  { get; }
        DateTime  LastExecuteEnd    { get; }

        void  ConcludeExecute ();
        void  ExecuteIfItCan  (object param = null);

        Func<object, Task> AsyncTask { get; }
        Task RunAsync(object arg = null);
    }
}
