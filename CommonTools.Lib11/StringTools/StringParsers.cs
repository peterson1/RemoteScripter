using CommonTools.Lib11.ExceptionTools;

namespace CommonTools.Lib11.StringTools
{
    public static class StringParsers
    {
        public static int ToInt(this string text)
        {
            if (int.TryParse(text, out int num))
                return num;
            else
                throw Fault.BadCast(text, num);
        }
    }
}
