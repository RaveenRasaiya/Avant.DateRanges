using Avant.Application.Interfaces;
using System;

namespace Avant.Application
{
    public class DateRangeValidationService : IDateRangeValidationService
    {
        public void Validate(DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue)
            {
                throw new ArgumentException($"Invalid {nameof(startDate)}");
            }
            if (endDate == DateTime.MinValue)
            {
                throw new ArgumentException($"Invalid {nameof(endDate)}");
            }
            if (endDate.Date < startDate.Date)
            {
                throw new ArgumentException($"{nameof(endDate)} should be greater than {nameof(startDate)}");
            }
            if (endDate.Date == startDate.Date)
            {
                throw new ArgumentException($"{nameof(endDate)} can't be same as {nameof(startDate)}");
            }
        }
    }
}
