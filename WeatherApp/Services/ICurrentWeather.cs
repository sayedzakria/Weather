using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
namespace WeatherApp.Services
{
    public interface  ICurrentWeather
    {
        Task AddCurrentWeather(CurrentWeather currentWeather);
       
        Task<CurrentWeather> GetCurrentWeather(int id);
        Task RemoveCurrentWeather(int id);
    }
}
