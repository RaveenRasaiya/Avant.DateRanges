using Avant.Application.Extensions;
using Avant.Application.Interfaces;
using System;

namespace Avant.Application
{
    public class FollowingWeekHolidayService : IHolidayService
    {
        public bool HasHoliday(DateTime holiday, DateTime startDate, DateTime endDate)
        {
            if (!holiday.Date.IsWeekend())
            {
                return false;
            }
            holiday = holiday.Date.StartOfWeek(DayOfWeek.Monday);

            if (holiday.Date.IsWithInRange(startDate.Date, endDate.Date))
            {
                return true;
            }

            return false;
        }
    }
}
