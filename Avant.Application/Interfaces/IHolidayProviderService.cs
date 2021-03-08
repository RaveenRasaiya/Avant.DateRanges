using Avant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avant.Application.Interfaces
{
    public interface IHolidayProviderService
    {
        IHolidayService GetService(HolidayType holidayType);
    }
}
