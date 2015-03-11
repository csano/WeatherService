using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeatherService.Tests
{
    public abstract class WeatherServiceTests
    {
        public abstract IWeatherService Subject { get; }

        [TestMethod]
        public async Task TemperatureIsInExpectedRange()
        {
            var result = await Subject.GetWeatherAsync("Seattle, WA");
            // Roughly the hottest and coldest ever recorded, plus a margin
            Assert.IsTrue(result.Temperature > 150 && result.Temperature < 350);
        }

        [TestMethod]
        public async Task QueryingForForecastHasExpectedLocation()
        {
            var result = await Subject.GetForecast("Seattle, WA", 7);

            result.Location.City.ShouldBeEquivalentTo("Seattle");
        }

        [TestMethod]
        public async Task QueryingForForecastCapturesDatesForNextFiveDays()
        {
            var result = await Subject.GetForecast("Seattle, WA", 5);

            result.Data.Count.ShouldBeEquivalentTo(5);
            var currentDate = DateTime.Now.Date;
            foreach (var data in result.Data)
            {
                data.Date.ShouldBeEquivalentTo(currentDate);
                currentDate = currentDate.AddDays(1);
            }
        }

        [TestMethod]
        public async Task QueryingForForecastCapturesHighAndLowTemperaturesForNextFiveDays()
        {
            var result = await Subject.GetForecast("Seattle, WA", 5);

            result.Data.Count.ShouldBeEquivalentTo(5);
            foreach (var data in result.Data)
            {
                data.Temperature.High.Should().BeGreaterThan(0);
                data.Temperature.Low.Should().BeGreaterThan(0);
            }
        }
    }

    [TestClass]
    public class OpenweathermapWeatherServiceTests : WeatherServiceTests
    {
        private readonly IWeatherService _subject = new OpenWeatherMapWeatherService();

        public override IWeatherService Subject
        {
            get { return _subject; }
        }
    }

    [TestClass]
    public class WeatherServiceSimulatorTests : WeatherServiceTests
    {
        private readonly IWeatherService _subject = new WeatherServiceSimulator();

        public override IWeatherService Subject
        {
            get { return _subject; }
        }
    }

    class WeatherServiceSimulator : IWeatherService
    {
        public async Task<Weather> GetWeatherAsync(string query)
        {
            return new Weather(new Location()) { Temperature = 300 };
        }

        public async Task<Forecast> GetForecast(string query, int days)
        {
            var temperature = new Temperature { High = 300, Low = 225 };
            return new Forecast(new Location { City = "Seattle" })
            {
                Data = new List<ForecastData>
                {
                    new ForecastData { Date = DateTime.Now.Date, Temperature = temperature },
                    new ForecastData { Date = DateTime.Now.Date.AddDays(1), Temperature = temperature },
                    new ForecastData { Date = DateTime.Now.Date.AddDays(2), Temperature = temperature },
                    new ForecastData { Date = DateTime.Now.Date.AddDays(3), Temperature = temperature },
                    new ForecastData { Date = DateTime.Now.Date.AddDays(4), Temperature = temperature }
                }
            };
        }
    }
}