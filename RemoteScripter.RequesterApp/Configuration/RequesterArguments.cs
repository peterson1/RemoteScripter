using CommonTools.Lib11.StringTools;
using CommonTools.Lib45.FileSystemTools;
using CommonTools.Lib45.ThreadTools;
using Mono.Options;
using System;
using static RemoteScripter.RequesterApp.Properties.Settings;

namespace RemoteScripter.RequesterApp.Configuration
{
    class RequesterArguments : IHasUpdatedCopy
    {
        public RequesterArguments()
        {
            Parse(Environment.GetCommandLineArgs());

            if (UpdatedCopyPath.IsBlank())
                UpdatedCopyPath = Default.UpdatedCopyPath;
        }


        public string  UpdatedCopyPath   { get; private set; }
        //public string  RequestsFilePath  { get; private set; }


        private void Parse(string[] commandLineArgs)
        {
            var options = new OptionSet
            {
                {"exe|origexe="  , "Original exe path" , exe => UpdatedCopyPath = exe  },
            };
            try
            {
                options.Parse(commandLineArgs);
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }
    }
}
