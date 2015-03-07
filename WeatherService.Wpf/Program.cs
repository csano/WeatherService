using System;
using WeatherService;

namespace WpfApplication1
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            var view = CreateView();
            view.ShowDialog();
        }

        public static MainWindow CreateView()
        {
            var model = new WeatherModel(new OpenweathermapWeatherService());
            var view = new MainWindow {DataContext = model.ViewModel};
            return view;
        }
    }
}
