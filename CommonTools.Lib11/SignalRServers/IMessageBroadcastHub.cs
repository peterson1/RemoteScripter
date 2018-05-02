using CommonTools.Lib11.DataStructures;
using CommonTools.Lib11.SignalRClients;

namespace CommonTools.Lib11.SignalRServers
{
    public interface IMessageBroadcastHub
    {
        void ReceiveClientState(CurrentClientState clientState);
        void ReceiveException  (ExceptionReport exceptionReport);
    }
}
