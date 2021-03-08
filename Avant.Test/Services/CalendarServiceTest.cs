using Avant.Application;
using Avant.Application.Interfaces;
using Avant.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Xunit;

namespace Avant.Test
{
    public class CalendarServiceTest
    {
        private readonly ICalendarService _calendarService;
        private readonly IDateRangeValidationService _dateRangeValidationService;
        private readonly IHolidayProviderService _holidayProviderService;
        private readonly Mock<IFileService> _holidayService;
        public CalendarServiceTest()
        {
            _dateRangeValidationService = new DateRangeValidationService();
            _holidayService = new Mock<IFileService>();
            _holidayProviderService = new HolidayProviderService();
            _calendarService = new CalendarService(_dateRangeValidationService, _holidayProviderService);
        }

        [Fact]

        public void DateService_WeeksDaysInAWeek_ExcludeStartAndEnd()
        {
            var startDate = new DateTime(2021, 03, 09);
            var endDate = new DateTime(2021, 03, 14);

            //act
            var result = _calendarService.GetWeekDays(startDate, endDate, true);
            result.Should().Be(3);
        }

        [Fact]

        public void DateService_WeeksDaysInAWeek_Not_ExcludeStartAndEnd()
        {
            var startDate = new DateTime(2021, 03, 09);
            var endDate = new DateTime(2021, 03, 14);

            //act
            var result = _calendarService.GetWeekDays(startDate, endDate, false);
            result.Should().Be(4);
        }


        [Fact]
        public void DateService_In_TwoWeeks()
        {
            var startDate = new DateTime(2021, 03, 1);
            var endDate = new DateTime(2021, 03, 14);

            //act
            var result = _calendarService.GetWeekDays(startDate, endDate, true);
            result.Should().Be(9);
        }


        [Fact]
        public void DateService_Start_Weekends()
        {
            var startDate = new DateTime(2021, 03, 06);
            var endDate = new DateTime(2021, 03, 21);

            //act
            var result = _calendarService.GetWeekDays(startDate, endDate, true);
            result.Should().Be(10);
        }

        [Fact]
        public void DateService_BusinessDays_WithHolidays()
        {
            var startDate = new DateTime(2021, 03, 01);
            var endDate = new DateTime(2021, 03, 31);

            List<Holiday> holidays = PrepareHolidays();

            _holidayService.Setup(f => f.GetHolidays(It.IsAny<string>())).Returns(holidays);

            //act
            var result = _calendarService.GetBusinessDays(startDate, endDate, true, holidays);
            result.Should().Be(19);
        }

        [Fact]
        public void DateService_BusinessDays_WithOutHolidays()
        {
            var startDate = new DateTime(2021, 03, 01);
            var endDate = new DateTime(2021, 03, 31);

            IEnumerable<Holiday> holidays = Enumerable.Empty<Holiday>();

            _holidayService.Setup(f => f.GetHolidays(It.IsAny<string>())).Returns(holidays);

            //act
            var result = _calendarService.GetBusinessDays(startDate, endDate, true, holidays);
            result.Should().Be(21);
        }

        [Fact]
        public void DateService_BusinessDays_OneYear()
        {
            var startDate = new DateTime(2021, 03, 01);
            var endDate = new DateTime(2022, 03, 01);

            List<Holiday> holidays = PrepareHolidays();
            AppendMoreHolidays(holidays);

            _holidayService.Setup(f => f.GetHolidays(It.IsAny<string>())).Returns(holidays);

            //act         
            var result = _calendarService.GetBusinessDays(startDate, endDate, true, holidays);
            result.Should().Be(256);
        }

        [Fact]
        public void DateService_BusinessDays_OneYear_Performance()
        {
            var startDate = new DateTime(2021, 03, 01);
            var endDate = new DateTime(2022, 03, 01);

            List<Holiday> holidays = PrepareHolidays();
            AppendMoreHolidays(holidays);

            _holidayService.Setup(f => f.GetHolidays(It.IsAny<string>())).Returns(holidays);

            //act
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = _calendarService.GetBusinessDays(startDate, endDate, true, holidays);
            stopwatch.Stop();
            result.Should().Be(256);
            stopwatch.Elapsed.Should().BeLessOrEqualTo(TimeSpan.FromMilliseconds(50));
        }

        private static void AppendMoreHolidays(List<Holiday> holidays)
        {
            holidays.AddRange(new List<Holiday>
            {
                new Holiday
                {
                    Day=24,
                    Month=9,
                    Type=Domain.Enums.HolidayType.Fixed
                },
                 new Holiday
                {
                    Day=23,
                    Month=10,
                    Type=Domain.Enums.HolidayType.FollowingWeekDay
                }
            });
        }
        private static List<Holiday> PrepareHolidays()
        {
            return new List<Holiday>
            {
                new Holiday
                {
                    Day=4,
                    Month=3,
                    Type=Domain.Enums.HolidayType.Fixed
                },
                new Holiday
                {
                    Day=7,
                    Month=3,
                    Type=Domain.Enums.HolidayType.AlwaysSameDay
                }
            };
        }
    }
}
