using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherService;

namespace WpfApplication1
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        public SimpleAsyncCommand GetWeatherCommand { get; private set; }

        private Location _location;
        public Location Location
        {
            get { return _location; }
            set { OnPropertyChanged(); _location = value; }
        }

        private double _temperature;

        public WeatherViewModel()
        {
            GetWeatherCommand = new SimpleAsyncCommand();
        }

        public double Temperature
        {
            get { return _temperature; }
            set { OnPropertyChanged(); _temperature = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
