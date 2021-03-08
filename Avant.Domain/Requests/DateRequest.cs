using System;

namespace Avant.Domain.Requests
{
    public class DateRequest
    {
        public DateTime StartDate { get; set; }
        public bool ExcludeHolidays { get; set; }
        public bool ExcludeStartAndEndDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
