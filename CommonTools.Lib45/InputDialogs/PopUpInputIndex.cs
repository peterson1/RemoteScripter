using System.Collections.Generic;
using System.Linq;

namespace CommonTools.Lib45.InputDialogs
{
    internal class PopUpInputIndex<TItems> : PopUpInput<int, PopUpInputIndexWindow>
    {
        internal PopUpInputIndex(string caption, string message, IEnumerable<TItems> options, int? defaultIndex) : base(caption, message)
        {
            Draft = defaultIndex ?? -1;
            Options.AddRange(options.Select(_ => _?.ToString() ?? "--"));
        }


        public List<string>  Options  { get; } = new List<string>();
        public int           Draft    { get; set; }


        public override bool TryParseValue(out int parsed)
        {
            parsed = Draft;
            if (Draft < 0)
            {
                WhyInvalid = "An option should be selected.";
                return false;
            }
            return true;
        }


        protected override void OnWindowLoad(PopUpInputIndexWindow win)
        {
            if (Draft < 0)
                win.cmbOptions.IsDropDownOpen = true;
        }
    }
}
