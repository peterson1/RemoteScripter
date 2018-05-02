using CommonTools.Lib11.EventHandlerTools;
using CommonTools.Lib11.ExceptionTools;
using CommonTools.Lib11.StringTools;
using CommonTools.Lib45.FileSystemTools;
using LiteDB;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CommonTools.Lib45.LiteDbTools
{
    public class SharedLiteDB
    {
        public event EventHandler DbChanged = delegate { };
        private MemoryStream      _mem;
        private FileSystemWatcher _watchr;
        private bool              _isDelaying;


        public SharedLiteDB(string dbFilePath, string currentUser)
        {
            if (dbFilePath.IsBlank())
                throw Fault.NullRef("DB File Path");

            DbPath = dbFilePath;

            InitializeCommons(currentUser);

            if (!File.Exists(DbPath))
                Metadata.CreateInitialRecord();

            InitializeFileWatcher();
        }

        public SharedLiteDB(MemoryStream memoryStream, string currentUser)
        {
            _mem = memoryStream;
            InitializeCommons(currentUser);
        }


        public string              DbPath       { get; }
        public string              CurrentUser  { get; private set; }
        public MetadataCollection  Metadata     { get; private set; }


        public LiteDatabase OpenRead()
            => Connect(LiteDB.FileMode.ReadOnly);


        public bool IsInMemory => _mem != null;


        public LiteDatabase OpenWrite()
            => Connect(LiteDB.FileMode.Shared);


        private LiteDatabase Connect(LiteDB.FileMode fileMode)
            => _mem != null
              ? new LiteDatabase(_mem)
              : new LiteDatabase(new ConnectionString
              {
                  Filename = DbPath,
                  Journal = false,
                  Mode = fileMode,
                  LimitSize = long.MaxValue
              });


        private void InitializeFileWatcher()
        {
            var abs = DbPath.MakeAbsolute();
            var dir = Path.GetDirectoryName(abs);
            var nme = Path.GetFileName(abs);

            _watchr = new FileSystemWatcher(dir, nme);
            _watchr.NotifyFilter = NotifyFilters.LastWrite;
            _watchr.Changed += async (s, e) =>
            {
                if (!_isDelaying)
                {
                    _isDelaying = true;
                    await Task.Delay(1000);
                    DbChanged.Raise();
                    _isDelaying = false;
                }
            };
            _watchr.EnableRaisingEvents = true;
        }


        private void InitializeCommons(string currentUser)
        {
            CurrentUser = currentUser;
            Metadata = new MetadataCollection(this);

            if (!File.Exists(DbPath))
                Metadata.CreateInitialRecord();
        }
    }
}
