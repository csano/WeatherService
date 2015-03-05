using System;
using System.Globalization;
using System.Windows.Forms;

namespace WeatherService.UI
{
    public partial class Form1 : Form
    {
        private readonly OpenweathermapWeatherService _weatherService;

        public Form1()
        {
            InitializeComponent();
            _weatherService = new OpenweathermapWeatherService();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var weather = await _weatherService.GetWeatherAsync("Seattle,WA");

            temperature.Text = weather.Temperature.ToString(CultureInfo.InvariantCulture);
            location.Text = weather.Location.City;
        }
    }
}
