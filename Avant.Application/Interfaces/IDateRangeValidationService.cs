using System;

namespace Avant.Application.Interfaces
{
    public interface IDateRangeValidationService
    {
        void Validate(DateTime startDate, DateTime endDate);
    }
}
