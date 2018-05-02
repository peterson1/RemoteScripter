using CommonTools.Lib11.StringTools;
using CommonTools.Lib45.ThreadTools;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace CommonTools.Lib45.FileSystemTools
{
    public class CurrentExeTools
    {
    }


    public static class CurrentExe
    {
        public static string GetFullPath()
            => Assembly.GetEntryAssembly()?.Location;


        public static string GetShortName()
            => Path.GetFileNameWithoutExtension(GetFullPath());


        public static string GetDirectory()
        {
            var exe = GetFullPath();
            if (exe.IsBlank()) return string.Empty;
            return Directory.GetParent(exe).FullName;
        }


        public static string GetVersion()
        {
            var exe = GetFullPath();
            return exe.IsBlank() ? "" : exe.GetVersion();
        }


        public static string GetShortVersion()
        {
            var exe = GetFullPath();
            return exe.IsBlank() ? "" : exe.GetShortVersion();
        }


        public static string ShortNameAndVersion()
            => $"{GetShortName()} v{GetShortVersion()}";


        public static void Shutdown()
            => UIThread.Run(() 
                => Application.Current.Shutdown());


        public static void RelaunchApp()
        {
            Process.Start(GetFullPath());
            CurrentExe.Shutdown();
        }


        public static Process   GetProcess    () => Process.GetCurrentProcess();
        public static Process[] GetProcesses  () => Process.GetProcessesByName(GetProcessName());
        public static string    GetProcessName() => GetProcess().ProcessName;


        public static int CountRunningInstances() 
            => GetProcesses().Length;


        public static bool HasAnotherInstance()
            => CountRunningInstances() > 1;
    }
}
