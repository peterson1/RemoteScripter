using CommonTools.Lib11.StringTools;
using CommonTools.Lib45.FileSystemTools;
using CommonTools.Lib45.ThreadTools;
using PropertyChanged;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace CommonTools.Lib45.InputCommands
{
    [AddINotifyPropertyChangedInterface]
    public class ExeLauncherCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = delegate { };


        public ExeLauncherCommand(string label, string exeFilepath, string arguments = null)
        {
            ExePath   = exeFilepath;
            Arguments = arguments;
            Label     = label;

            if (!ExePath.IsBlank())
            {
                ExePath = ExePath.MakeAbsolute();
                ExeName = Path.GetFileName(ExePath);
            }

            UpdateVersionInfo();
        }


        public string  ExePath    { get; }
        public string  ExeName    { get; }
        public string  Arguments  { get; }
        public string  Label      { get; }
        public string  Version    { get; private set; }
        public string  Status     { get; private set; }


        public bool CanExecute(object parameter) 
            => File.Exists(ExePath);


        public virtual void Execute(object parameter)
        {
            if (!File.Exists(ExePath)) return;
            StartExeProcess(ExePath);
            UpdateVersionInfo();
        }


        protected virtual void StartExeProcess(string exePath)
        {
            if (Arguments.IsBlank())
                Process.Start(exePath);
            else
                Process.Start(exePath, Arguments);
        }


        protected void UpdateVersionInfo()
            => Version = File.Exists(ExePath)
                       ? "v" + ExePath.GetShortVersion()
                       : "Missing: " + ExeName;


        protected void SetStatus(string text) => Status = text;
    }
}
