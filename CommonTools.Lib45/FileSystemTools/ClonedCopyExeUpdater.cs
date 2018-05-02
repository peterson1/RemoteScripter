using CommonTools.Lib11.FileSystemTools;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CommonTools.Lib45.FileSystemTools
{
    public class ClonedCopyExeUpdater
    {
        private const string BACKUP_DIR = "Clone_Backups";

        //private      EventHandler _copyUpdated;
        //public event EventHandler  CopyUpdated
        //{
        //    add    { _copyUpdated -= value; _copyUpdated += value; }
        //    remove { _copyUpdated -= value; }
        //}

        private IThrottledFileWatcher _watchr;


        public ClonedCopyExeUpdater(IThrottledFileWatcher throttledFileWatcher)
        {
            ClonedCopy = CurrentExe.GetFullPath();
            _watchr    = throttledFileWatcher;
            _watchr.FileChanged += OnMasterCopyChanged;
        }


        public string   ClonedCopy    { get; }
        public string   MasterCopy    => _watchr.TargetFile;
        public uint     SecondsDelay  { get; set; } = 3;


        private async void OnMasterCopyChanged(object sender, EventArgs e)
        {
            //  wait for the dust to settle
            //
            await Task.Delay(TimeSpan.FromSeconds(SecondsDelay));

            //  move clone to backup dir
            //
            var backupPath = CreateBackupPath(ClonedCopy);
            File.Move(ClonedCopy, backupPath);

            //  copy master to clone path
            //
            File.Copy(MasterCopy, ClonedCopy);

            //_copyUpdated.Raise();
        }


        private string CreateBackupPath(string targetPath)
        {
            var dir = GetOrCreateBackupDir(targetPath);
            var pre = DateTime.Now.ToString("yyyy-MM-dd_hhmmss");
            var nme = Path.GetFileNameWithoutExtension(targetPath);
            var ext = "backup";
            return Path.Combine(dir, $"{pre}_{nme}.{ext}");
        }


        private string GetOrCreateBackupDir(string targetPath)
        {
            var baseDir = Path.GetDirectoryName(targetPath);
            var bkpDir = Path.Combine(baseDir, BACKUP_DIR);
            Directory.CreateDirectory(bkpDir);
            return bkpDir;
        }


        public void StartWatching(string masterCopyPath)
            => _watchr.StartWatching(masterCopyPath);


        public void StopWatching() => _watchr.StopWatching();
    }
}
