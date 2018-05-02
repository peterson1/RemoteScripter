using System;

namespace CommonTools.Lib11.FileSystemTools
{
    public interface IFileChangeWatcher
    {
        event EventHandler FileChanged;

        void   StartWatching  (string filepath);
        void   StopWatching   ();

        string TargetFile     { get; }
    }
}
