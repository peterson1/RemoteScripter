using System;
using System.Threading.Tasks;

namespace CommonTools.Lib11.SignalRClients
{
    public interface IHubClientProxy : IDisposable
    {
        event EventHandler<string> StateChanged;

        Task  Connect    ();
        void  Disconnect ();
    }
}
