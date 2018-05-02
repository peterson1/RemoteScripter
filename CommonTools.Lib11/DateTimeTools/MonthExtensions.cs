using System;

namespace CommonTools.Lib11.DateTimeTools
{
    public static class MonthExtensions
    {
        public static DateTime MonthFirstDay(this DateTime date)
            => new DateTime(date.Year, date.Month, 1);

        public static DateTime MonthLastDay(this DateTime date)
            => new DateTime(date.Year, date.Month, 
                DateTime.DaysInMonth(date.Year, date.Month));
    }
}
