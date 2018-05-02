namespace CommonTools.Lib11.MathTools
{
    public static class PercentExtensions
    {
        public static decimal PercentOf(this decimal numerator, decimal denominator)
        {
            if (denominator == 0) return 0;
            return numerator / denominator;
        }


        public static decimal PercentOf(this int numerator, int denominator)
            => ((decimal)numerator).PercentOf((decimal)denominator);
    }
}
