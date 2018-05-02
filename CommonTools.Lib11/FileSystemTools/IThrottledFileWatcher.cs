namespace CommonTools.Lib11.FileSystemTools
{
    public interface IThrottledFileWatcher : IFileChangeWatcher
    {
        uint IntervalMS { get; set; }
    }
}
