namespace CommonTools.Lib11.LocalStorage
{
    public interface ILiteDbSettings
    {
        string  DbFilePath      { get; }
        string  CollectionName  { get; }
    }
}
