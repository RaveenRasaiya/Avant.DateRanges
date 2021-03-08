using System;

namespace Avant.Application.Extensions
{
    public static class DateExtensions
    {
        public static bool IsWeekend(this DateTime dateTime)
        {
            return (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }


        public static bool IsWithInRange(this DateTime inputDate, DateTime startDate, DateTime endDate)
        {
            return inputDate > startDate && inputDate < endDate;
        }
    }
}
