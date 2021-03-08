using Avant.Domain.Requests;
using Avent.Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Avant.Test.Controllers
{
    public class CalendarControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly WebApplicationFactory<Startup> _factory;
        private const string endPointUrl = "/api/v1/calendar/";
        private const string mediaType = "application/json";

        public CalendarControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]

        public async Task CalendarController_Not_Send_Request_Parameter()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}weekdays";

            var content = new StringContent(string.Empty, Encoding.UTF8, mediaType);
            // Act
            var response = await client.PostAsync(url, content);
            // Assert            
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]

        public async Task CalendarController_Not_SetAnyDates()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}weekdays";

            var content = new StringContent(JsonConvert.SerializeObject(new DateRequest
            {
                ExcludeHolidays = true,
                ExcludeStartAndEndDate = true
            }), Encoding.UTF8, mediaType);
            // Act
            var response = await client.PostAsync(url, content);
            // Assert            
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]

        public async Task CalendarController_WeekDays_SameDate()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}weekdays";

            var content = new StringContent(JsonConvert.SerializeObject(new DateRequest
            {
                StartDate = new DateTime(2021, 03, 01),
                EndDate = new DateTime(2021, 03, 01),
                ExcludeHolidays = true,
                ExcludeStartAndEndDate = true
            }), Encoding.UTF8, mediaType);
            // Act
            var response = await client.PostAsync(url, content);
            // Assert            
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }


        [Fact]

        public async Task CalendarController_WeekDays_InAWeek()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}weekdays";

            var content = new StringContent(JsonConvert.SerializeObject(new DateRequest
            {
                StartDate = new DateTime(2021, 03, 01),
                EndDate = new DateTime(2021, 03, 07),
                ExcludeStartAndEndDate = true
            }), Encoding.UTF8, mediaType);
            // Act
            var response = await client.PostAsync(url, content);
            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNullOrEmpty();
            responseContent.Should().BeEquivalentTo("4");
        }


        [Fact]

        public async Task CalendarController_WeekDays_InAWeek_IncludeStartAndEnd()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}weekdays";

            var content = new StringContent(JsonConvert.SerializeObject(new DateRequest
            {
                StartDate = new DateTime(2021, 03, 01),
                EndDate = new DateTime(2021, 03, 07),
                ExcludeStartAndEndDate = false
            }), Encoding.UTF8, mediaType);
            // Act
            var response = await client.PostAsync(url, content);
            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNullOrEmpty();
            responseContent.Should().BeEquivalentTo("5");
        }



        [Fact]

        public async Task CalendarController_WeekDays_InAYear()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}weekdays";

            var content = new StringContent(JsonConvert.SerializeObject(new DateRequest
            {
                StartDate = new DateTime(2021, 03, 01),
                EndDate = new DateTime(2022, 03, 01),
                ExcludeStartAndEndDate = true
            }), Encoding.UTF8, mediaType);
            // Act
            var response = await client.PostAsync(url, content);
            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNullOrEmpty();
            responseContent.Should().BeEquivalentTo("260");
        }


        [Fact]

        public async Task CalendarController_BusinessDays_WithHolidays()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}businessdays";

            var content = new StringContent(JsonConvert.SerializeObject(new DateRequest
            {
                StartDate = new DateTime(2013, 03, 01),
                EndDate = new DateTime(2013, 12, 31),
                ExcludeHolidays = true,
                ExcludeStartAndEndDate = true
            }), Encoding.UTF8, mediaType);
            // Act
            var response = await client.PostAsync(url, content);
            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNullOrEmpty();
            responseContent.Should().BeEquivalentTo("213");
        }

        [Fact]

        public async Task CalendarController_BusinessDays_WithOutHolidays()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}businessdays";

            var content = new StringContent(JsonConvert.SerializeObject(new DateRequest
            {
                StartDate = new DateTime(2013, 03, 01),
                EndDate = new DateTime(2013, 12, 31),
                ExcludeHolidays = false,
                ExcludeStartAndEndDate = true
            }), Encoding.UTF8, mediaType);
            // Act
            var response = await client.PostAsync(url, content);
            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNullOrEmpty();
            responseContent.Should().BeEquivalentTo("215");
        }


        [Fact]

        public async Task CalendarController_BusinessDays_WithOutHolidays_IncludeStartAndEndDate()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}businessdays";

            var content = new StringContent(JsonConvert.SerializeObject(new DateRequest
            {
                StartDate = new DateTime(2013, 03, 01),
                EndDate = new DateTime(2013, 12, 31),
                ExcludeHolidays = false,
                ExcludeStartAndEndDate = false
            }), Encoding.UTF8, mediaType);
            // Act
            var response = await client.PostAsync(url, content);
            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNullOrEmpty();
            responseContent.Should().BeEquivalentTo("218");
        }

    }
}
