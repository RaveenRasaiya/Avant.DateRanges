using Avant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avant.Application.Interfaces
{
    public interface IHolidayService
    {
        IEnumerable<Holiday> GetHolidays(string sourceFilePath);
    }
}
