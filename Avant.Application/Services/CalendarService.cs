using Avant.Application.Extensions;
using Avant.Application.Interfaces;
using Avant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avant.Application
{
    public class CalendarService : ICalendarService
    {
        private readonly IDateRangeValidationService _dateRangeValidationService;
        private readonly IHolidayProviderService _holidayProviderService;

        public CalendarService(IDateRangeValidationService dateRangeValidationService, IHolidayProviderService holidayProviderService)
        {
            _dateRangeValidationService = dateRangeValidationService;
            _holidayProviderService = holidayProviderService;
        }

        public int GetWeekDays(DateTime startDate, DateTime endDate, bool excludeStartEndDay)
        {
            if (excludeStartEndDay)
            {
                startDate = startDate.AddDays(1);
                endDate = endDate.AddDays(-1);
            }

            _dateRangeValidationService.Validate(startDate, endDate);

            int numberOfDays = 1 + Convert.ToInt32((endDate - startDate).TotalDays);

            int weekEndDays = (numberOfDays + Convert.ToInt32(startDate.DayOfWeek)) / 7;

            return numberOfDays - 2 * weekEndDays - (startDate.DayOfWeek == DayOfWeek.Sunday ? 1 : 0) + (endDate.DayOfWeek == DayOfWeek.Saturday ? 1 : 0);
        }

        public int GetBusinessDays(DateTime startDate, DateTime endDate, bool excludeStartEndDay, IEnumerable<Holiday> holidays)
        {

            _dateRangeValidationService.Validate(startDate, endDate);

            var noOfWeekDays = GetWeekDays(startDate, endDate, excludeStartEndDay);

            if (holidays != null && holidays.Any())
            {
                noOfWeekDays = ProcessHolidays(startDate, endDate, holidays, noOfWeekDays);
            }

            return noOfWeekDays;
        }

        public int ProcessHolidays(DateTime startDate, DateTime endDate, IEnumerable<Holiday> holidays, int noOfWeekDays)
        {
            foreach (var holiday in holidays)
            {
                for (int year = startDate.Year; year <= endDate.Year; year++)
                {
                    var _holidayDate = new DateTime(year, holiday.Month, holiday.Day);
                    var holidayService = _holidayProviderService.GetService(holiday.Type);
                    if (holidayService == null)
                    {
                        continue;
                    }
                    if (holidayService.HasHoliday(_holidayDate, startDate.Date, endDate.Date))
                    {
                        noOfWeekDays -= 1;
                    }
                }
            }

            return noOfWeekDays;
        }

    }
}
