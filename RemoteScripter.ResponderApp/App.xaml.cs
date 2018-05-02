using CommonTools.Lib45.ApplicationTools;
using RemoteScripter.ResponderApp.Configuration;
using System.Windows;

namespace RemoteScripter.ResponderApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.Initialize<ResponderArguments>(args =>
            {
                new ResponderMainVM(args).Show<MainWindow>();
            });
        }
    }
}
