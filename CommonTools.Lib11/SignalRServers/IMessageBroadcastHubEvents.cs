using System.Threading.Tasks;

namespace CommonTools.Lib11.SignalRServers
{
    public interface IMessageBroadcastHubEvents
    {
        Task  BroadcastMessage   (string subject, string message);
        Task  RewriteConfigFile  (string encryptedDTO);
    }
}
