using CommonTools.Lib45.FileSystemTools;
using CommonTools.Lib45.ThreadTools;
using Mono.Options;
using System;

namespace RemoteScripter.ResponderApp.Configuration
{
    class ResponderArguments : IHasUpdatedCopy
    {
        public ResponderArguments()
        {
            Parse(Environment.GetCommandLineArgs());
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
