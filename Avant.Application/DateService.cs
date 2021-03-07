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
        public int GetWeekDays(DateTime startDate, DateTime endDate)
        {
            _dateRangeValidationService.Validate(startDate, endDate);

            var noOfWeekDays = Enumerable.Range(1, (endDate - startDate).Days-1)
                                .Select(day => startDate.AddDays(day))
                                .Where(day => !day.IsWeekend())
                                .Count();
                                
            return noOfWeekDays;
        }

        public int GetBusinessDays(DateTime startDate, DateTime endDate, IEnumerable<Holiday> holidays)
        {
            throw new NotImplementedException();
        }
    }
}
