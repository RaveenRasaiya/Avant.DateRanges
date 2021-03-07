using Avant.Application.Interfaces;
using Avant.Domain.Entities;
using System;
using System.Collections.Generic;

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

            int ndays = 1 + Convert.ToInt32((endDate - startDate).TotalDays);
            int nsaturdays = (ndays + Convert.ToInt32(startDate.DayOfWeek)) / 7;
            return ndays - 2 * nsaturdays
                   - (startDate.DayOfWeek == DayOfWeek.Sunday ? 1 : 0)
                   + (endDate.DayOfWeek == DayOfWeek.Saturday ? 1 : 0);
        }

        public int GetBusinessDays(DateTime startDate, DateTime endDate, bool excludeStartEndDay, IEnumerable<Holiday> holidays)
        {
            _dateRangeValidationService.Validate(startDate, endDate);

            var noOfWeekDays = GetWeekDays(startDate, endDate, excludeStartEndDay);

            return noOfWeekDays;
        }
    }
}
