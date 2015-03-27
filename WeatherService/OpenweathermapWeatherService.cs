using System;
using System.Collections.Generic;
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
        Task<Forecast> GetForecast(string query, int days);
    }

    public class OpenWeatherMapWeatherService : IWeatherService
    {
       public async Task<Forecast> GetForecast(string query, int days)
       {
           Forecast forecast;
           using (var client = new HttpClient())
           {
               client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/");

               var response = await client.GetAsync(string.Format("forecast/daily?q={0}&mode=xml&cnt={1}", query, days));

               if (!response.IsSuccessStatusCode) return null;

               var output = await response.Content.ReadAsStringAsync();
               var doc = XDocument.Parse(output);
               var city = doc.XPathSelectElement("/weatherdata/location/name").Value;
               var location = new Location { City = city };
               forecast = new Forecast(location);
               foreach (var timeElement in doc.XPathSelectElements("/weatherdata/forecast/time"))
               {
                   forecast.Data.Add(new ForecastData
                   {
                       Date = DateTime.Parse(timeElement.Attribute("day").Value),
                       Temperature = new Temperature
                       {
                           High = Double.Parse(timeElement.XPathSelectElement("temperature").Attribute("max").Value),
                           Low = Double.Parse(timeElement.XPathSelectElement("temperature").Attribute("min").Value)
                       },
                       Icon =  timeElement.XPathSelectElement("symbol").Attribute("var").Value
                   });
               }
           }

           return forecast;
        }

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

    public class ForecastData
    {
        public DateTime Date { get; set; }
        public Temperature Temperature { get; set; }
        public string Icon { get; set; }
    }

    public class Forecast
    {
        public Location Location { get; private set; }
        public List<ForecastData> Data { get; set; } 

        public Forecast(Location location)
        {
            Location = location;
            Data = new List<ForecastData>();
        }
    }

    public class Temperature
    {
        public double High { get; set; }
        public double Low { get; set; }
    }
}