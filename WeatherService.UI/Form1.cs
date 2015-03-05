using System;
using System.Globalization;
using System.Windows.Forms;

namespace WeatherService.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var apiProxy = new OpenweathermapWeatherService();
            var weather = await apiProxy.GetWeatherAsync("Seattle,WA");

            temperature.Text = weather.Temperature.ToString(CultureInfo.InvariantCulture);
            location.Text = weather.Location.City;
        }
    }
}
