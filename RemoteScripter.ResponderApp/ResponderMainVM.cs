using CommonTools.Lib11.StringTools;
using CommonTools.Lib45.BaseViewModels;
using CommonTools.Lib45.FileSystemTools;
using PropertyChanged;
using RemoteScripter.ResponderApp.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static RemoteScripter.ResponderApp.Properties.Settings;

namespace RemoteScripter.ResponderApp
{
    [AddINotifyPropertyChangedInterface]
    class ResponderMainVM : UpdatedExeVMBase<ResponderArguments>
    {
        private FileSystemWatcher _watchr;
        protected override string CaptionPrefix => "RS Responder";


        public ResponderMainVM(ResponderArguments appArguments) : base(appArguments)
        {
            ProcessPath = Default.ProcessPathToRun;

            UpdateNotifier.ExecuteOnFileChanged = true;
            CreateMissingFiles();

            _watchr = Default.RequestsFilePath
                .OnFileChanged(() => OnChangeDetected());
            
            ClickRefresh();
        }


        public string  CommandToRun { get; private set; }
        public string  ProcessPath  { get; }


        protected override void OnRefreshClicked()
        {
            CommandToRun = File.ReadAllText(ProcessPath);
        }


        private void CreateMissingFiles()
        {
            Default.RequestsFilePath.CreateFileIfMissing();
            Default.ResponsesFilePath.CreateFileIfMissing();
            Default.ProcessPathToRun.CreateFileIfMissing();
        }


        private void OnChangeDetected()
        {
            var reqs  = Default.RequestsFilePath;
            var key   = File.ReadLines(reqs).Last().Trim();
            var resps = Default.ResponsesFilePath;
            if (!resps.FileContains(key))
                ProcessRequest(key);
        }


        private void ProcessRequest(string requestKey)
        {
            StartBeingBusy("Responding to request ...");

            File.AppendAllText(Default
                .ResponsesFilePath, L.f + requestKey);

            Process.Start(ProcessPath);

            StopBeingBusy();

        }
    }
}
