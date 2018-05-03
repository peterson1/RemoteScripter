using PropertyChanged;
using CommonTools.Lib11.StringTools;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace CommonTools.Lib45.FileSystemTools
{
    [AddINotifyPropertyChangedInterface]
    public class UpdatedExeNotifier : UpdatedFileNotifier
    {
        private string _args;
        private string _tempExe;

        public UpdatedExeNotifier(string fileToWatch) : base(fileToWatch)
        {
            _args = GetCommandLineArgs();
            RelaunchIfOutdated();
        }


        private void RelaunchIfOutdated()
        {
            if (WatchedFile.IsBlank()) return;
            if (!File.Exists(WatchedFile)) return;
            var thisHash   = CurrentExe.GetFullPath().SHA1ForFile();
            var watchdHash = WatchedFile.SHA1ForFile();
            if (thisHash == watchdHash) return;
            OnFileChanged();
            OnExecuteClick();
        }


        private string GetCommandLineArgs()
            => string.Join(" ", Environment.GetCommandLineArgs().Skip(1)
                .Select(_ => Quotify(_)));


        private string Quotify(string soloArg)
        {
            var pos = soloArg.IndexOf('=');
            var key = soloArg.Substring(0, pos);
            var val = soloArg.Substring(pos + 1);

            if (val.Contains(" "))
                val = $"\"{val}\"";

            return $"{key}={val}";
        }


        protected override void OnFileChanged() 
            => _tempExe = CopyWatchedToTemp();


        protected override void OnExecuteClick()
        {
            Process.Start(_tempExe, _args);
            Application.Current.Shutdown();
        }


        private string CopyWatchedToTemp()
        {
            var tmp = Path.GetTempFileName();
            File.Delete(tmp);
            tmp += ".exe";
            File.Copy(WatchedFile, tmp, true);
            return tmp;
        }
    }
}
