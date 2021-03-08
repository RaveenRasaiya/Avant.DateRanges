using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Avant.Domain.Requests
{
    public class DateRequest : IValidatableObject
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]

        public DateTime StartDate { get; set; }
        public bool ExcludeHolidays { get; set; }
        public bool ExcludeStartAndEndDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this == null)
            {
                yield return new ValidationResult($"Reequest can't be null");
            }
            if (StartDate == default)
            {
                yield return new ValidationResult($"{nameof(StartDate)} is not valid.", new[] { nameof(StartDate) });
            }
            if (EndDate == default)
            {
                yield return new ValidationResult($"{nameof(EndDate)} is not valid.", new[] { nameof(EndDate) });
            }
        }
    }
}
