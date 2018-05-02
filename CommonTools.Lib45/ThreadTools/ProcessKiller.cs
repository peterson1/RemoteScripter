using System.Diagnostics;

namespace CommonTools.Lib45.ThreadTools
{
    public class KillProcess
    {
        public static void ByName(string processName, bool terminateForcefully)
        {
            var frc = terminateForcefully ? "/f" : "";
            var cmd = $"taskkill {frc} /im {processName}";
            Process.Start("cmd.exe", $"/C {cmd}");
        }
    }
}
