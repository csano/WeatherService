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
            return new Forecast(new Location { City = "Seattle" });
        }
    }
}
