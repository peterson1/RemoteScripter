using System;

namespace CommonTools.Lib11.DateTimeTools
{
    public static class DateFactoryExtensions
    {
        public static DateTime Jan(this int day, int year) => new DateTime(year, 1, day);
        public static DateTime Feb(this int day, int year) => new DateTime(year, 2, day);
        public static DateTime Mar(this int day, int year) => new DateTime(year, 3, day);
        public static DateTime Apr(this int day, int year) => new DateTime(year, 4, day);
        public static DateTime May(this int day, int year) => new DateTime(year, 5, day);
        public static DateTime Jun(this int day, int year) => new DateTime(year, 6, day);
        public static DateTime Jul(this int day, int year) => new DateTime(year, 7, day);
        public static DateTime Aug(this int day, int year) => new DateTime(year, 8, day);
        public static DateTime Sep(this int day, int year) => new DateTime(year, 9, day);
        public static DateTime Oct(this int day, int year) => new DateTime(year, 10, day);
        public static DateTime Nov(this int day, int year) => new DateTime(year, 11, day);
        public static DateTime Dec(this int day, int year) => new DateTime(year, 12, day);
    }
}
