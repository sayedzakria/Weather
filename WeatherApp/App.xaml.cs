using WeatherApp.Services;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<ICurrentWeather,CurrentWeatherService>();
            DependencyService.Register<IInternetWeather, InternetWeather>();
            
            MainPage = new AppShell();
        }
    }
}
