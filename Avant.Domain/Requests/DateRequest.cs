using System;

namespace Avant.Domain.Requests
{
    public class DateRequest
    {
        public DateTime StartDate { get; set; }
        public bool IncludeHolidays { get; set; }
        public DateTime EndDate { get; set; }
    }
}
