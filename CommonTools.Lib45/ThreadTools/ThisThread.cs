using System.Globalization;
using System.Threading;

namespace CommonTools.Lib45.ThreadTools
{
    public class ThisThread
    {
        public static void SetShortDateFormat(string dateFormat)
        {
            var ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = dateFormat;
            Thread.CurrentThread.CurrentCulture = ci;
        }
    }
}
