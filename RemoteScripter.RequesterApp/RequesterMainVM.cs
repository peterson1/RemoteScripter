using CommonTools.Lib11.InputCommands;
using CommonTools.Lib45.BaseViewModels;
using CommonTools.Lib45.InputCommands;
using PropertyChanged;
using RemoteScripter.RequesterApp.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using static RemoteScripter.RequesterApp.Properties.Settings;

namespace RemoteScripter.RequesterApp
{
    [AddINotifyPropertyChangedInterface]
    class RequesterMainVM : UpdatedExeVMBase<RequesterArguments>
    {
        protected override string CaptionPrefix => "RS Requester";

        private string _reqKey;


        public RequesterMainVM(RequesterArguments appArguments) : base(appArguments)
        {
            SendRequestCmd = R2Command.Relay(SendRequest, _ => !IsBusy, "Send Request");
            CheckResponseCmd = R2Command.Async(CheckResponse, null, "Check Response");
            GenerateNewRequestKey();
        }

        public IR2Command  SendRequestCmd    { get; }
        public IR2Command  CheckResponseCmd  { get; }


        private void SendRequest()
        {
            UpdateWatchedFile();
            CheckResponseCmd.ExecuteIfItCan();
        }


        private void UpdateWatchedFile()
        {
            var file = Default.RequestsFilePath;
            File.AppendAllText(file, _reqKey);
        }


        private async Task CheckResponse()
        {
            await Task.Delay(2000);
            throw new NotImplementedException();
        }


        private void GenerateNewRequestKey()
            => _reqKey = DateTime.Now.ToLongTimeString();
    }
}
