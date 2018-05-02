namespace CommonTools.Lib45.InputDialogs
{
    internal class PopUpInputInt : PopUpInput<int, PopUpInputIntWindow>
    {
        internal PopUpInputInt(string caption, string message, int? defaultVal) : base(caption, message)
        {
            Draft = defaultVal;
        }


        public int? Draft { get; set; }


        public override bool TryParseValue(out int parsed)
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


        protected override void OnWindowLoad(PopUpInputIntWindow win)
            => win.txtVal.SelectAll();
    }
}
