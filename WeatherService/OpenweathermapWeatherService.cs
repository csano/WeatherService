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