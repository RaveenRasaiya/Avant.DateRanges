using Avant.Application.Interfaces;
using Avant.Domain.Entities;
using Avant.Domain.Requests;
using Avent.Api.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Avent.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CalendarController : BaseController
    {
        private readonly ILogger<CalendarController> _logger;
        private readonly IDateService _dateService;
        private readonly IHolidayService _holidayService;
        private readonly HolidaySetting _holidaySetting;


        public CalendarController(ILogger<CalendarController> logger, IOptions<HolidaySetting> options, IDateService dateService, IHolidayService holidayService)
        {
            _logger = logger;
            _dateService = dateService;
            _holidayService = holidayService;
            _holidaySetting = options.Value;
        }
               

        [HttpPost("weekdays")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult WeekDays([FromBody] DateRequest dateRequest)
        {
            Validate(dateRequest);
            return Ok(_dateService.GetWeekDays(dateRequest.StartDate, dateRequest.EndDate, dateRequest.ExcludeStartAndEndDate));
        }

        [HttpPost("businessdays")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult BusinessDays([FromBody] DateRequest dateRequest)
        {
            Validate(dateRequest);
            IEnumerable<Holiday> holidays = Enumerable.Empty<Holiday>();
            if (dateRequest.ExcludeHolidays)
            {
                holidays = _holidayService.GetHolidays(_holidaySetting.SourceFile);
            }
            return Ok(_dateService.GetBusinessDays(dateRequest.StartDate, dateRequest.EndDate, dateRequest.ExcludeStartAndEndDate, holidays));
        }
    }
}
