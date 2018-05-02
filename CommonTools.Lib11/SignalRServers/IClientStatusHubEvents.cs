using CommonTools.Lib11.SignalRClients;

namespace CommonTools.Lib11.SignalRServers
{
    public interface IClientStatusHubEvents
    {
        void ClientConnected    (HubClientSession sessionInfo);
        void ClientInteracted   (HubClientSession sessionInfo);
        void ClientDisconnected (HubClientSession sessionInfo);
    }
}
