using CommonTools.Lib11.InputCommands;
using CommonTools.Lib11.StringTools;
using CommonTools.Lib45.BaseViewModels;
using CommonTools.Lib45.FileSystemTools;
using CommonTools.Lib45.InputCommands;
using PropertyChanged;
using RemoteScripter.RequesterApp.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static RemoteScripter.RequesterApp.Properties.Settings;

namespace RemoteScripter.RequesterApp
{
    [AddINotifyPropertyChangedInterface]
    class RequesterMainVM : UpdatedExeVMBase<RequesterArguments>
    {
        protected override string CaptionPrefix => "RS Requester";


        public RequesterMainVM(RequesterArguments appArguments) : base(appArguments)
        {
            CheckDelayMS      = Default.CheckDelayMS;
            RequestingMessage = Default.RequestingMessage;
            RequestAndWaitCmd = R2Command.Async(RequestAndWait, _ => !IsBusy, "Re-send the Request");
            RequestAndWaitCmd.ExecuteIfItCan();
        }


        public IR2Command  RequestAndWaitCmd  { get; }
        public string      BadMessage         { get; private set; }
        public int         CheckDelayMS       { get; }
        public string      RequestingMessage  { get; }


        private async Task RequestAndWait()
        {
            BadMessage = "";
            StartBeingBusy(RequestingMessage);

            if (await SendAndCheckTwice())
            {
                MessageBox.Show("Target received the request.");
                CurrentExe.Shutdown();
            }
            else
                BadMessage = "Target did not respond to the request.";

            StopBeingBusy();
        }


        private async Task<bool> SendAndCheckTwice()
        {
            var key = SendNewRequest();

            await Task.Delay(CheckDelayMS);
            if (ResponsesContainsKey(key)) return true;

            StartBeingBusy("Waiting for response ...");
            await Task.Delay(CheckDelayMS);

            return ResponsesContainsKey(key);
        }


        private bool ResponsesContainsKey(string key)
        {
            var file = Default.ResponsesFilePath;
            if (!File.Exists(file)) return false;
            var text = key.Trim();
            return File.ReadLines(file)
                .Any(_ => _.Contains(text));
        }


        private string SendNewRequest()
        {
            var file = Default.RequestsFilePath;
            var key  = GenerateNewRequestKey();
            File.AppendAllText(file, key);
            return key;
        }


        private string GenerateNewRequestKey()
        {
            var now = DateTime.Now;
            return L.f + now.ToLongDateString()
                + ", " + now.ToLongTimeString();
        }
    }
}
