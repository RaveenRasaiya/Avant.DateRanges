using System;
using System.ComponentModel.DataAnnotations;

namespace Avant.Domain.Requests
{
    public class DateRequest
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        public bool ExcludeHolidays { get; set; }
        public bool ExcludeStartAndEndDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
    }
}
