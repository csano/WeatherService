using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeatherService.Tests
{
    [TestClass]
    public class OpenweathermapWeatherServiceTests
    {
        [TestMethod]
        public async Task TemperatureIsInExpectedRange()
        {
            var subject = new OpenweathermapWeatherService();
            var result = await subject.GetWeatherAsync("Seattle, WA");
            // Roughly the hottest and coldest ever recorded, plus a margin
            Assert.IsTrue(result.Temperature > 150 && result.Temperature < 350);
        }
    }
}
