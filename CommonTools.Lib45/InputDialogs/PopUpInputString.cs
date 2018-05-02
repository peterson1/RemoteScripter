using CommonTools.Lib11.StringTools;

namespace CommonTools.Lib45.InputDialogs
{
    internal class PopUpInputString : PopUpInput<string, PopUpInputStringWindow>
    {
        internal PopUpInputString(string caption, string message, string defaultVal) : base(caption, message)
        {
            Draft = defaultVal;
        }


        public string  Draft  { get; set; }


        public override bool TryParseValue(out string parsed)
        {
            parsed = Draft;
            if (Draft.IsBlank())
            {
                WhyInvalid = "Value should not be blank.";
                return false;
            }
            return true;
        }
    }
}
