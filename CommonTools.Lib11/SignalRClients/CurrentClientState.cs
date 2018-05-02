using System.Collections.Generic;

namespace CommonTools.Lib11.SignalRClients
{
    public class CurrentClientState
    {
        public string  PublicIP       { get; set; }
        public string  PrivateIPs     { get; set; }
        public string  ScreenshotB64  { get; set; }

        public List<CurrentExeState>  ExeStates  { get; set; }
    }
}
