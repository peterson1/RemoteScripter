using CommonTools.Lib11.InputCommands;
using CommonTools.Lib11.StringTools;
using CommonTools.Lib45.InputCommands;
using CommonTools.Lib45.ThreadTools;
using PropertyChanged;
using System.IO;
using System.Threading.Tasks;

namespace CommonTools.Lib45.FileSystemTools
{
    [AddINotifyPropertyChangedInterface]
    public class UpdatedFileNotifier
    {
        private FileSystemWatcher _watchr;
        private bool              _isDelaying;


        public UpdatedFileNotifier(string fileToWatch)
        {
            WatchedFile = fileToWatch;
            ExecuteCmd  = R2Command.Relay(OnExecuteClick);
            if (!fileToWatch.IsBlank())
                InitializeFileWatcher();
        }


        public string      WatchedFile          { get; }
        public IR2Command  ExecuteCmd           { get; }
        public bool        IsFileChanged        { get; private set; }
        public bool        ExecuteOnFileChanged { get; set; }
        public int         NotifyDelayMS        { get; set; } = 2000;


        protected virtual void OnExecuteClick () { }


        protected virtual void OnFileChanged()
        {
            if (ExecuteOnFileChanged)
            {
                UIThread.Run(() =>
                    ExecuteCmd.ExecuteIfItCan());
            }
        }


        private void InitializeFileWatcher()
        {
            if (WatchedFile.IsBlank()) return;
            if (!File.Exists(WatchedFile)) return;
            var abs = WatchedFile.MakeAbsolute();
            var dir = Path.GetDirectoryName(abs);
            var nme = Path.GetFileName(abs);

            _watchr              = new FileSystemWatcher(dir, nme);
            _watchr.NotifyFilter = NotifyFilters.LastWrite;
            _watchr.Changed     += async (s, e) =>
            {
                if (!_isDelaying)
                {
                    _isDelaying   = true;
                    await Task.Delay(NotifyDelayMS);
                    IsFileChanged = true;
                    OnFileChanged();
                    _isDelaying   = false;
                }
            };
            _watchr.EnableRaisingEvents = true;
        }
    }
}
