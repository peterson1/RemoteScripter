using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTools.Lib45.InputDialogs
{
    internal class PopUpInputDecimal : PopUpInput<decimal, PopUpInputDecimalWindow>
    {
        internal PopUpInputDecimal(string caption, string message, decimal? defaultVal) : base(caption, message)
        {
            Draft = defaultVal;
        }


        public decimal? Draft { get; set; }


        public override bool TryParseValue(out decimal parsed)
        {
            if (!Draft.HasValue)
            {
                parsed     = 0;
                WhyInvalid = "Value should not be blank.";
                return false;
            }
            parsed = Draft.Value;
            return true;
        }


        protected override void OnWindowLoad(PopUpInputDecimalWindow win)
            => win.txtVal.SelectAll();
    }
}
