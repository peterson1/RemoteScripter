using PropertyChanged;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using CommonTools.Lib11.StringTools;
using static System.Environment;

namespace CommonTools.Lib45.FileSystemTools
{
    [AddINotifyPropertyChangedInterface]
    public class UpdatedExeNotifier : UpdatedFileNotifier
    {

        public UpdatedExeNotifier(string fileToWatch) : base(fileToWatch)
        {
            if (!CurrentExe.GetFullPath().IsInTempDir())
                RelaunchInTemp();
        }


        private void RelaunchInTemp()
        {
            if (WatchedFile.IsBlank()) return;
            var exeNow = CurrentExe.GetFullPath();
            var cfgNow = exeNow + ".config";
            var tmpExe = WatchedFile.MakeTempCopy(".exe");
            var tmpCfg = tmpExe + ".config";

            if (File.Exists(cfgNow))
                File.Copy(cfgNow, tmpCfg, true);

            Process.Start(tmpExe, GetCommandLineArgs());
            Application.Current.Shutdown();
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


        protected override void OnExecuteClick() 
            => RelaunchInTemp();
    }
}
