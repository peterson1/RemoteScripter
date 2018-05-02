using CommonTools.Lib45.BaseViewModels;
using RemoteScripter.ResponderApp.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteScripter.ResponderApp
{
    class ResponderMainVM : UpdatedExeVMBase<ResponderArguments>
    {
        protected override string CaptionPrefix => "RS Responder";


        public ResponderMainVM(ResponderArguments appArguments) : base(appArguments)
        {
        }

    }
}
