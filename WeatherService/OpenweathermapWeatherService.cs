using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace WeatherService
{
    public class Location
    {
        public string City { get; set; }
    }

    public class Weather
    {
        public Weather(Location location)
        {
            Location = location; 
        }
        public Location Location { get; private set; }
        public double Temperature { get; set; } 
    }

    public interface IWeatherService
    {
        Task<Weather> GetWeatherAsync(string query);
    }

    // class that represents resource at 
    // http://api.openweathermap.org/data/2.5/weather?q=London&mode=xml
    public class WeatherResource
    {
        private readonly IWeatherService weatherService;
        public WeatherResource(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        public Weather CurrentForLocation(string location)
        {
            // modify weatherService.GetWeatherAsync naming to reflect that it just handles requests/returns a response
            // GetWeatherAsync returns XML data as a string
            // parse XML via XDocument.Parse
            // create Weather object/return it
            return null;
        }
    }

    // class that represents resource at 
    // http://api.openweathermap.org/data/2.5/forecast/daily?q=London&mode=xml&units=metric&cnt=7
    public class ForecastResource // can refactor to base resource
    {
        private readonly IWeatherService weatherService;
        public ForecastResource(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        // object for now, need to come up w/DTO
        public object ForecastForLocation(string location, int numberOfDays)
        {
            return null;
        }
    }

    public class OpenWeatherMapWeatherService : IWeatherService
    {
        public async Task<Weather> GetWeatherAsync(string query)
        {
            Weather weather;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/");

                var response = await client.GetAsync(string.Format("weather/?q={0}&mode=xml", query));

                if (!response.IsSuccessStatusCode) return null;

                var output = await response.Content.ReadAsStringAsync();
                var doc = XDocument.Parse(output);
                var city = doc.XPathSelectElement("/current/city").Attribute("name").Value;
                var temperature = Double.Parse(doc.XPathSelectElement("/current/temperature").Attribute("value").Value);
                var location = new Location { City = city };
                weather = new Weather(location) { Temperature = temperature };
            }

            return weather;
        }
    }
}