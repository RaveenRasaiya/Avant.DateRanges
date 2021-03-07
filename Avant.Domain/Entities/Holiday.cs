using Avant.Domain.Enums;
using System;

namespace Avant.Domain.Entities
{
    public class Holiday
    {
        public DateTime Date { get; set; }
        public HolidayType Type { get; set; }
    }
}
