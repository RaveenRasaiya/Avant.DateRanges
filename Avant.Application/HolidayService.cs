using Avant.Application.Interfaces;
using Avant.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;

namespace Avant.Application
{
    public class HolidayService : IHolidayService
    {
        public IEnumerable<Holiday> GetHolidays(string sourceFilePath)
        {
            if (string.IsNullOrWhiteSpace(sourceFilePath))
            {
                throw new ArgumentNullException(nameof(sourceFilePath));
            }
            if (!File.Exists(sourceFilePath))
            {
                throw new FileNotFoundException($"{sourceFilePath} is not found");
            }
            return JsonConvert.DeserializeObject<IEnumerable<Holiday>>(File.ReadAllText(sourceFilePath));
        }
    }
}
