using System;

namespace CommonTools.Lib45.InputDialogs
{
    internal class PopUpInputDate : PopUpInput<DateTime, PopUpInputDateWindow>
    {
        internal PopUpInputDate(string caption, string message, DateTime? defaultVal) : base(caption, message)
        {
            Draft = defaultVal;
        }


        public DateTime? Draft { get; set; }


        public override bool TryParseValue(out DateTime parsed)
        {
            if (!Draft.HasValue)
            {
                parsed     = DateTime.MinValue;
                WhyInvalid = "Date should not be blank.";
                return false;
            }
            parsed = Draft.Value;
            return true;
        }


        protected override void OnWindowLoad(PopUpInputDateWindow win)
            => win.pickr.IsDropDownOpen = true;
    }
}
