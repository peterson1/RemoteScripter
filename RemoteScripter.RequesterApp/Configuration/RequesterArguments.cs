﻿using CommonTools.Lib45.FileSystemTools;
using CommonTools.Lib45.ThreadTools;
using Mono.Options;
using System;

namespace RemoteScripter.RequesterApp.Configuration
{
    class RequesterArguments : IHasUpdatedCopy
    {
        public RequesterArguments()
        {
            //Parse(Environment.GetCommandLineArgs());
        }


        public string  UpdatedCopyPath   { get; }
        //public string  RequestsFilePath  { get; private set; }


        //private void Parse(string[] commandLineArgs)
        //{
        //    var options = new OptionSet
        //    {
        //        {"exe|origexe="  , "Original exe path" , exe => UpdatedCopyPath = exe  },
        //    };
        //    try
        //    {
        //        options.Parse(commandLineArgs);
        //    }
        //    catch (Exception ex)
        //    {
        //        Alert.Show(ex.Message);
        //    }
        //}
    }
}