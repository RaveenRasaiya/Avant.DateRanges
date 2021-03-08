using Avant.Application.Extensions;
using Avant.Application.Interfaces;
using Avant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avant.Application
{
    public class DateService : IDateService
    {
        private readonly IDateRangeValidationService _dateRangeValidationService;

        public DateService(IDateRangeValidationService dateRangeValidationService)
        {
            _dateRangeValidationService = dateRangeValidationService;
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
            if (excludeStartEndDay)
            {
                startDate = startDate.AddDays(1);
                endDate = endDate.AddDays(-1);
            }

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
                    if (holiday.Type == Domain.Enums.HolidayType.Fixed && _holidayDate.Date.IsWithInRange(startDate.Date, endDate.Date) && !_holidayDate.Date.IsWeekend())
                    {
                        noOfWeekDays -= 1;
                    }
                    else if (holiday.Type == Domain.Enums.HolidayType.FollowingWeekDay && _holidayDate.Date.IsWeekend())
                    {
                        _holidayDate = _holidayDate.Date.StartOfWeek(DayOfWeek.Monday);

                        if (_holidayDate.Date.IsWithInRange(startDate.Date, endDate.Date))
                        {
                            noOfWeekDays -= 1;
                        }
                    }
                    else if (holiday.Type == Domain.Enums.HolidayType.AlwaysSameDay && _holidayDate.Date.IsWithInRange(startDate.Date, endDate.Date))
                    {
                        noOfWeekDays -= 1;
                    }
                }
            }

            return noOfWeekDays;
        }

    }
}
