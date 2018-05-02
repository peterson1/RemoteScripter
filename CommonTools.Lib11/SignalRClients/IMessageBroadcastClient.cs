using System;
using System.Collections.Generic;

namespace CommonTools.Lib11.SignalRClients
{
    public interface IMessageBroadcastClient : IHubClientProxy
    {
        event EventHandler<KeyValuePair<string, string>> BroadcastReceived;
        event EventHandler<string> ConfigRewriteRequested;

        void  SendClientState (CurrentClientState state);
        void  SendException   (string context, Exception ex);
    }
}
