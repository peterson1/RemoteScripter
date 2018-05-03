using CommonTools.Lib45.FileSystemTools;
using CommonTools.Lib45.ThreadTools;
using CommonTools.Lib11.StringTools;
using Mono.Options;
using System;
using static RemoteScripter.ResponderApp.Properties.Settings;

namespace RemoteScripter.ResponderApp.Configuration
{
    class ResponderArguments : IHasUpdatedCopy
    {
        public ResponderArguments()
        {
            Parse(Environment.GetCommandLineArgs());

            if (UpdatedCopyPath.IsBlank())
                UpdatedCopyPath = Default.UpdatedCopyPath;
        }


        public string UpdatedCopyPath { get; private set; }


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
