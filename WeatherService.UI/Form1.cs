using System;
using System.Globalization;
using System.Windows.Forms;

namespace WeatherService.UI
{
    public partial class Form1 : Form
    {
        private readonly IWeatherService _weatherService;

        public Form1(IWeatherService weatherService)
        {
            InitializeComponent();
            _weatherService = weatherService;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var weather = await _weatherService.GetWeatherAsync("Seattle,WA");

            temperature.Text = weather.Temperature.ToString(CultureInfo.InvariantCulture);
            location.Text = weather.Location.City;
        }
    }
}
