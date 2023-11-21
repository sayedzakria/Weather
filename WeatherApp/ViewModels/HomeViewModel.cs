using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp.ViewModels
{
    internal class HomeViewModel : BaseViewModel
    {
        CurrentWeather _currentWeather;
        public CurrentWeather CurrentWeather
        {
            get => _currentWeather;
            set => SetProperty(ref _currentWeather, value);
        }
        ICurrentWeather currentWeatherService;
        IInternetWeather internetWeatherService;
        public HomeViewModel()
        {
            Title = "Home";
            currentWeatherService = DependencyService.Get<ICurrentWeather>();
            internetWeatherService = DependencyService.Get<IInternetWeather>();
           // initdatabase();
        }

        async void initdatabase()
        {
             
                CurrentWeather = await currentWeatherService.GetCurrentWeather(1);
                if (CurrentWeather == null)
                {

                    //Call online Api
                    string city = Preferences.Get(nameof(city), "London");
                    CurrentWeather = await internetWeatherService.GetCurrentWeather(city, "en");
                    if (CurrentWeather == null)
                    {
                        //save to sqlite databse
                        await currentWeatherService.AddCurrentWeather(CurrentWeather);
                    }
                }
             
        }
    }
}
