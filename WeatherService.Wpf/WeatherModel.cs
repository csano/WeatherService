using System.Threading.Tasks;
using WeatherService;

namespace WpfApplication1
{
    public class WeatherModel
    {
        private readonly IWeatherService _weatherService;
        public readonly WeatherViewModel ViewModel;

        public WeatherModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;
            ViewModel = new WeatherViewModel
            {
                GetWeatherCommand =
                {
                    AsyncOperation = GetWeather
                }
            };
        }

        public async Task GetSevenDayForecast()
        {
            //var weather = await _weatherService.GetWeatherAsync("Seattle, WA");
            //ViewModel.Location = weather.Location;
            //ViewModel.Temperature = weather.Temperature;
        }

        public async Task GetWeather()
        {
            var weather = await _weatherService.GetWeatherAsync("Seattle, WA");
            ViewModel.Location = weather.Location;
            ViewModel.Temperature = weather.Temperature;
        }
    }
}