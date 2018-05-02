using CommonTools.Lib45.BaseViewModels;
using RemoteScripter.RequesterApp.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteScripter.RequesterApp
{
    class RequesterMainVM : UpdatedExeVMBase<RequesterArguments>
    {
        protected override string CaptionPrefix => "RS Requester";


        public RequesterMainVM(RequesterArguments appArguments) : base(appArguments)
        {
        }
    }
}
