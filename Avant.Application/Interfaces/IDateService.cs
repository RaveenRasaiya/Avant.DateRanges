using Avant.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Avant.Application.Interfaces
{
    public interface IDateService
    {
        int GetWeekDays(DateTime startDate, DateTime endDate);
        int GetBusinessDays(DateTime startDate, DateTime endDate, IEnumerable<Holiday> holidays);
    }
}
