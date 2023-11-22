using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    class ForcastViewModel : BaseViewModel
    {
        IInternetWeather internetWeatherService;
       AllForcast allForcast;
        public AllForcast Allforcast
        {
            get => allForcast;
            set => SetProperty(ref allForcast, value);
        }
        public ForcastViewModel()
        {
            internetWeatherService = DependencyService.Get<IInternetWeather>();
            initAsync();
        }

        async Task initAsync()
        {
            string city = Preferences.Get(nameof(city), "tanta");
            Allforcast = await internetWeatherService.GetForcastWeather(city, "5");
            foreach (var item in Allforcast.forecast.forecastday)
            {
                DateTime itemdate = DateTime.Parse(item.date);
                if (itemdate.Date == DateTime.Now.Date)
                {
                    item.day.BGColor = Color.FromHex("#2B0B98");
                    OnPropertyChanged();
                }
            }
        }
    }
}
