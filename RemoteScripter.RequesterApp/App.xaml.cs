using CommonTools.Lib45.ApplicationTools;
using RemoteScripter.RequesterApp.Configuration;
using System.Windows;

namespace RemoteScripter.RequesterApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.Initialize<RequesterArguments>(args =>
            {
                new RequesterMainVM(args).Show<MainWindow>();
            });
        }
    }
}
