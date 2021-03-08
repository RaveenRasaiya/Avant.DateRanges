using Avant.Application;
using Avant.Application.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Avant.Test
{
    public class DateRangeValidationServiceTest
    {
      
        private readonly IDateRangeValidationService _dateRangeValidationService;
        public DateRangeValidationServiceTest()
        {
            _dateRangeValidationService = new DateRangeValidationService();          
        }

        [Fact]
        public void DateRangeValidationService_StartDate_MinValue()
        {
            // arrange
            Action act = () => _dateRangeValidationService.Validate(DateTime.MinValue, DateTime.Now.AddDays(-1));
            //asert
            act.Should().Throw<ArgumentException>().And.Message.Should().Be("Invalid startDate");
        }


        [Fact]
        public void DateRangeValidationService_EndDate_MinValue()
        {
            // arrange
            Action act = () => _dateRangeValidationService.Validate(DateTime.Now, DateTime.MinValue);
            //asert
            act.Should().Throw<ArgumentException>().And.Message.Should().Be("Invalid endDate");
        }

        [Fact]
        public void DateRangeValidationService_InvalidDateRange()
        {
            // arrange
            Action act = () => _dateRangeValidationService.Validate(DateTime.Now, DateTime.Now.AddDays(-1));
            //asert
            act.Should().Throw<ArgumentException>().And.Message.Should().Be("endDate should be greater than startDate");
        }


        [Fact]
        public void DateRangeValidationService_SameDate()
        {
            // arrange
            Action act = () => _dateRangeValidationService.Validate(DateTime.Now, DateTime.Now);
            act.Should().Throw<ArgumentException>().And.Message.Should().Be("endDate can't be same as startDate");
        }

        [Fact]
        public void DateRangeValidationService_Valid_DateRange()
        {
            // arrange
            Action act = () => _dateRangeValidationService.Validate(DateTime.Now, DateTime.Now.AddDays(5));
            act.Should().NotThrow<ArgumentException>();
        }
    }
}
