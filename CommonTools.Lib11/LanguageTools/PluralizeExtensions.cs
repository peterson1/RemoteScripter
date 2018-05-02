namespace CommonTools.Lib11.LanguageTools
{
    public static class PluralizeExtensions
    {
        public static string Pluralize(this string singular, object number, bool prependNumber = true)
        {
            if (number == null) return $"NULL {singular}s";

            if (!int.TryParse(number.ToString(), out int num))
                return $"NaN {singular}s";

            var sufx = num == 1 ? singular : $"{singular}s";

            return prependNumber ? $"{num:N0} {sufx}" : sufx;
        }
    }
}
