namespace CommonTools.Lib11.SignalRClients
{
    public interface IHubClientSettings
    {
        string   ServerURL   { get; }
        string   SharedKey   { get; }
        string   UserAgent   { get; }

        string   ReadSavedFile  ();
    }
}
