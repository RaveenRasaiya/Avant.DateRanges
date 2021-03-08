using Avant.Application;
using Avant.Application.Interfaces;
using Avant.Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Avant.Test
{
    public class HolidayServiceTest
    {
        private readonly IHolidayService _holidayService;
        public HolidayServiceTest()
        {
            _holidayService = new HolidayService();
        }

        [Fact]
        public void HolidayService_EmptyFilePath()
        {
            //arrange
            var filePath = string.Empty;
            //act
            Action act = () => _holidayService.GetHolidays(filePath);
            //assert
            act.Should().Throw<ArgumentNullException>().And.Message.Should().Be("Value cannot be null. (Parameter 'sourceFilePath')");
        }

        [Fact]
        public void HolidayService_FileNotExists()
        {
            //arrange
            var filePath = @"C:\NotExists.json";
            //act
            Action act = () => _holidayService.GetHolidays(filePath);
            //assert
            act.Should().Throw<FileNotFoundException>().And.Message.Should().Be($"{filePath} is not found");
        }


        [Fact]
        public void HolidayService_FileExists()
        {
            //arrange
            var holidays = new List<Holiday>(){ new Holiday
            {
                Day = 1,
                Month = 10,
                Type = Domain.Enums.HolidayType.AlwaysSameDay
            } };
           
            var fileInfo = CreateTestFile(JsonConvert.SerializeObject(holidays));
            //act
            Action act = () => _holidayService.GetHolidays(fileInfo.FullName);

            //assert
            act.Should().NotThrow<FileNotFoundException>();
            fileInfo.Delete();
        }


        public FileInfo CreateTestFile(string content)
        {
            var emtptyFilePath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString() + ".json");
            var fileInfo = new FileInfo(emtptyFilePath);
            using (var fs = File.Create(emtptyFilePath))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(content);
                fs.Write(info, 0, info.Length);
            }
            return fileInfo;
        }
    }
}
