using System;

namespace CommonTools.Lib11.LanguageTools
{
    public static class NumberToWords
    {
        static string[] first =
        {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
            "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
            "Seventeen", "Eighteen", "Nineteen"
        };
        static string[] tens =
        {
            "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety",
        };


        /// <summary>
        /// Converts the given number to an english sentence.
        /// from http://stackoverflow.com/a/1691675
        /// </summary>
        /// <param name="n">The number to convert.</param>
        /// <returns>The string representation of the number.</returns>
        public static string ToWords(this int n)
            => (n == 0 ? first[n] : Step(n)).Trim();

        // traverse the number recursively
        private static string Step(int n)
            => n < 0           ? "Minus " + Step(-n) :
               n == 0          ? "" :
               n <= 19         ? first[n] :
               n <= 99         ? tens[n / 10 - 2] + " " + Step(n % 10) :
               n <= 199        ? "One Hundred " + Step(n % 100) :
               n <= 999        ? Step(n / 100) + " Hundred " + Step(n % 100) :
               n <= 1999       ? "One Thousand " + Step(n % 1000) :
               n <= 999999     ? Step(n / 1000) + " Thousand " + Step(n % 1000) :
               n <= 1999999    ? "One Million " + Step(n % 1000000) :
               n <= 999999999  ? Step(n / 1000000) + " Million " + Step(n % 1000000) :
               n <= 1999999999 ? "One Billion " + Step(n % 1000000000) :
                                   Step(n / 1000000000) + " Billion " + Step(n % 1000000000);


        public static string ToPesoWords(this decimal value)
        {
            var p1Val = (int)Math.Truncate(value);
            var p2Val = (int)Math.Truncate((value - p1Val) * 100);
            var p1Sfx = (p1Val == 1) ? "peso" : "pesos";
            var p2Sfx = (p2Val == 1) ? "centavo" : "centavos";
            var words = p1Val.ToWords() + " " + p1Sfx;

            if (p2Val != 0) words += " and "
                + p2Val.ToWords() + " " + p2Sfx;

            return words;
        }

        public static string ToPesoWords(this double value)
            => ((decimal)value).ToPesoWords();
    }
}
