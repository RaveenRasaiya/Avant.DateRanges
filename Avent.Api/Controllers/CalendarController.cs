using Avant.Application.Interfaces;
using Avant.Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Avent.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ILogger<CalendarController> _logger;
        private readonly IDateService _dateService;

        public CalendarController(ILogger<CalendarController> logger, IDateService dateService)
        {
            _logger = logger;
            _dateService = dateService;
        }
        [HttpPost("weekdays")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult WeekDays([FromBody] DateRequest dateRequest)
        {
            var startDate = new DateTime(2014, 8, 13);
            var endDate = new DateTime(2014, 8, 21);

            return Ok(_dateService.GetWeekDays(startDate, endDate, true));
        }

        [HttpPost("businessdays")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult BusinessDays([FromBody] DateRequest dateRequest)
        {

            var startDate = new DateTime(2014, 8, 7);
            var endDate = new DateTime(2014, 8, 11);

            return Ok(_dateService.GetWeekDays(startDate, endDate, true));
        }
    }
}
