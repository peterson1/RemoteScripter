using CommonTools.Lib11.StringTools;
using CommonTools.Lib45.BaseViewModels;
using CommonTools.Lib45.FileSystemTools;
using PropertyChanged;
using RemoteScripter.ResponderApp.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
                .OnFileChanged(async () => await OnChangeDetected());
            
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


        private async Task OnChangeDetected()
        {
            await Task.Delay(Default.OnChangeDelayMS);
            var reqs  = Default.RequestsFilePath;
            var key   = File.ReadLines(reqs).ToList().Last().Trim();
            var resps = Default.ResponsesFilePath;
            if (!resps.FileContains(key))
                await ProcessRequest(key);
        }


        private async Task ProcessRequest(string requestKey)
        {
            StartBeingBusy("Responding to request ...");
            Process.Start(ProcessPath);
            await PostResponse(requestKey, true);
            StopBeingBusy();

        }


        private async Task PostResponse(string requestKey, bool retryOnce)
        {
            try
            {
                File.AppendAllText(Default
                    .ResponsesFilePath, L.f + requestKey);
            }
            catch (IOException)
            {
                if (retryOnce)
                {
                    await Task.Delay(1000);
                    await PostResponse(requestKey, false);
                }
            }
        }
    }
}
