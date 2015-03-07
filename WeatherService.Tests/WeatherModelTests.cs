using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApplication1;
using zz_Support;

namespace WeatherService.Tests
{
    [TestClass]
    public class WeatherModelTests
    {
        [TestMethod]
        public async Task GetWeatherAndPopulateViewModel()
        {
            var subject = new WeatherModel(new WeatherServiceSimulator());
            await subject.GetWeather();
            Assert.AreEqual(300, subject.ViewModel.Temperature);
        }

        [TestMethod]
        public void CreateVmToMatchItself()
        {
            var subject = new WeatherModel(new WeatherServiceSimulator());
            var result = subject.ViewModel;
            result.GetWeatherCommand
                .Should().Call(subject.GetWeather);
        }
    }
}
