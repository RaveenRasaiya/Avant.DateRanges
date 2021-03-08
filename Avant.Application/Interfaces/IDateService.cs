using Avant.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Avant.Application.Interfaces
{
    public interface IDateService
    {
        int GetWeekDays(DateTime startDate, DateTime endDate, bool excludeStartEndDay);

        int GetBusinessDays(DateTime startDate, DateTime endDate, bool excludeStartEndDay, IEnumerable<Holiday> holidays);

        bool IsWithInRange(DateTime inputDate, DateTime startDate, DateTime endDate);

        int ProcessHolidays(DateTime startDate, DateTime endDate, IEnumerable<Holiday> holidays, int noOfWeekDays);
    }
}
