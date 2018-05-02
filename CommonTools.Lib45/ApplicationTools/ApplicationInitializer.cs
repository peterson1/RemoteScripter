using CommonTools.Lib11.GoogleTools;
using CommonTools.Lib45.BaseViewModels;
using CommonTools.Lib45.FileSystemTools;
using CommonTools.Lib45.ThreadTools;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CommonTools.Lib45.ApplicationTools
{
    public static class AppInitializer
    {
        public static void Initialize<TArg>(this Application app, Action<TArg> actionOnArguments)
            where TArg : ICredentialsProvider, new()
        {
            HandleGlobalErrors();
            ThisThread.SetShortDateFormat("d MMM yyyy");
            TArg args = default(TArg);
            SafeExecute(() => args = new TArg(), "Parsing arguments");
            SafeExecute(() => actionOnArguments(args), "Consuming arguments");

            //todo: find a place for this
            //app.Exit += (s, e) => RunOnExit(args);
            //AccessControlExtensions.OnUnauthorizedAccess = s => ShowNotAllowed(s);
        }


        private static void ShowNotAllowed(string msg)
            => MessageBox.Show(msg, "  Unauthorized Access", MessageBoxButton.OK, MessageBoxImage.Information);


        private static void SafeExecute(Action action, string context)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                Alert.Show(ex, context);
                CurrentExe.Shutdown();
            }
        }


        //private static void RunOnExit(ICredentialsProvider args)
        //{
        //}


        private static void HandleGlobalErrors()
        {
            Application.Current.DispatcherUnhandledException += (s, e) =>
            {
                e.Handled = true;
                OnError(e.Exception, "Application.Current");
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                e.SetObserved();
                OnError(e.Exception, "TaskScheduler");
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                OnError(e.ExceptionObject as Exception, "AppDomain.CurrentDomain");
                // application terminates after above
            };
        }


        private static void OnError(Exception ex, string context)
        {
            Alert.Show(ex, context);
        }
    }
}
