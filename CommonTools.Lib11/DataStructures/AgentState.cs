using System;

namespace CommonTools.Lib11.DataStructures
{
    public class AgentState
    {
        public string    ExeVersion     { get; set; }
        public string    ExeSHA1        { get; set; }
        public string    ScreenshotURL  { get; set; }
        public string    RunningTask    { get; set; }
        public DateTime  LastActivity   { get; set; }
    }
}
