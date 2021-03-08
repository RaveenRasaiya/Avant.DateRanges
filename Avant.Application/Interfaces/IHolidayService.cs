using System;

namespace Avant.Application.Interfaces
{
    public interface IHolidayService
    {
        bool HasHoliday(DateTime holiday, DateTime startDate, DateTime endDate);
    }
}
