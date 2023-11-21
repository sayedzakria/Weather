using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IInternetWeather
    {
        Task<CurrentWeather> GetCurrentWeather(string city,string lang);
        Task<List<Models.Location>> GetSearchCityWeather(string city);
    }
}
