using Avant.Application.Interfaces;
using System;

namespace Avant.Application
{
    public class DateRangeValidationService : IDateRangeValidationService
    {
        public void Validate(DateTime startDate, DateTime endDate)
        {
            if (endDate.Date < startDate.Date)
            {
                throw new ArgumentException($"{nameof(endDate)} should be greater than {nameof(startDate)}");
            }
            if (endDate.Date == startDate.Date)
            {
                throw new ArgumentException($"{nameof(startDate)} can't be  {nameof(endDate)}");
            }
        }
    }
}
