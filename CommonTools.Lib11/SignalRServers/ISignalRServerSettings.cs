namespace CommonTools.Lib11.SignalRServers
{
    public struct GlobalServer
    {
        public static ISignalRServerSettings Settings { get; set; }
    }


    public interface ISignalRServerSettings
    {
        string   ServerURL             { get; }
        string   SharedKey             { get; }
        string   MasterCopy            { get; }
        double   DisconnectTimeoutHrs  { get; }
    }
}
