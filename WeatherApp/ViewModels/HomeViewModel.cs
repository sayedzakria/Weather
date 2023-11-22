using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

        Models.Location selectedCity;
        public Models.Location SelectedCity
        {
            get => selectedCity;
            set => SetProperty(ref selectedCity, value);
        }
        public HomeViewModel()
        {
            Title = "Home";
            currentWeatherService = DependencyService.Get<ICurrentWeather>();
            internetWeatherService = DependencyService.Get<IInternetWeather>();
            // Retrieve the data when the SecondViewModel is constructed
        MessagingCenter.Subscribe<SearchViewModel, Models.Location>(this, "DataMessage", (sender, data) =>
        {
            selectedCity = data;

            CallOnlieApi();
        });
             initdatabase();
        }

        async void initdatabase()
        {
             DateTime lastupdat= Preferences.Get("LastUpdate", DateTime.Now);
           
            
                CurrentWeather = await currentWeatherService.GetCurrentWeather();
                if (CurrentWeather == null)
                {
             await   CallOnlieApi();


                }
                else
                {
                if (lastupdat != DateTime.Now)
                {
                    await CallOnlieApi();
                }
                CurrentWeather.Location = await currentWeatherService.GetLocation(CurrentWeather.LocationId);
                    CurrentWeather.Current = await currentWeatherService.GetCurrent(CurrentWeather.CurrentId);

                }
            
        }

        async Task CallOnlieApi()
        {
            //Call online Api

            string city = Preferences.Get("city", "tanta");
            CurrentWeather = await internetWeatherService.GetCurrentWeather(city, "en");
            if (CurrentWeather != null)
            {
                //save to sqlite databse
                int locationId = await currentWeatherService.AddLocation(CurrentWeather.Location);
                int conditionId = await currentWeatherService.AddCondition(CurrentWeather.Current.Condition);
                CurrentWeather.Current.ConditionId = conditionId;
                int currentId = await currentWeatherService.AddCurrent(CurrentWeather.Current);

                CurrentWeather.LocationId = locationId;
                CurrentWeather.CurrentId = currentId;

                await currentWeatherService.AddCurrentWeather(CurrentWeather);
                Preferences.Set("LastUpdate", DateTime.Now);
            }
        }
    }
}
