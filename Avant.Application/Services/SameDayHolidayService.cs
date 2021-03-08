using Avant.Application.Extensions;
using Avant.Application.Interfaces;
using System;

namespace Avant.Application
{
    public class SameDayHolidayService : IHolidayService
    {
        public bool HasHoliday(DateTime holiday, DateTime startDate, DateTime endDate)
        {
            return holiday.Date.IsWithInRange(startDate.Date, endDate.Date);
        }
    }
}
