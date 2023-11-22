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
        Task<int> AddCurrentWeather(CurrentWeather currentWeather);
        Task<int> AddCurrent(Current current);
        Task<int> AddLocation(Models.Location location);
        Task<int> AddCondition(Models.Condition condition);
        Task<CurrentWeather> GetCurrentWeather();
        Task<Current> GetCurrent(int id);
        Task<Models.Location> GetLocation(int id);
        Task<Models.Condition> GetCondition(int id);
        Task RemoveCurrentWeather(int id);
    }
}
