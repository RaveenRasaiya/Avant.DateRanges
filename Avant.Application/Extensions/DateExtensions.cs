using System;

namespace Avant.Application.Extensions
{
    public static class DateExtensions
    {
        public static bool IsWeekend(this DateTime dateTime)
        {
            return (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday);
        }
    }
}
