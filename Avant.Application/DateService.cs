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
                    DateTime _holidayDate = new(year, holiday.Date.Month, holiday.Date.Day);
                    if (holiday.Type == Domain.Enums.HolidayType.Fixed && IsWithInRange(_holidayDate.Date, startDate.Date, endDate.Date) && !_holidayDate.IsWeekend())
                    {
                        noOfWeekDays -= 1;
                    }
                    else if (holiday.Type == Domain.Enums.HolidayType.FollowingWeekDay && _holidayDate.IsWeekend())
                    {
                        _holidayDate = _holidayDate.StartOfWeek(DayOfWeek.Monday);

                        if (IsWithInRange(_holidayDate.Date, startDate.Date, endDate.Date))
                        {
                            noOfWeekDays -= 1;
                        }
                    }
                    else if (holiday.Type == Domain.Enums.HolidayType.AlwaysSameDay && IsWithInRange(_holidayDate.Date, startDate.Date, endDate.Date))
                    {
                        noOfWeekDays -= 1;
                    }
                }
            }

            return noOfWeekDays;
        }

        public bool IsWithInRange(DateTime inputDate, DateTime startDate, DateTime endDate)
        {
            return inputDate > startDate && inputDate < endDate;
        }
    }
}
