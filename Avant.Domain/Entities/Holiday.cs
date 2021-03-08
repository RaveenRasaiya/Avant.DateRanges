using Avant.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Avant.Domain.Entities
{
    public class Holiday
    {
        [Range(1, 31)]
        public int Day { get; set; }
        [Range(1, 12)]
        public int Month { get; set; }
        public HolidayType Type { get; set; }
    }
}
