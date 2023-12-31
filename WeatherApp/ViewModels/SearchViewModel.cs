﻿using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;
using WeatherApp.Views;

namespace WeatherApp.ViewModels
{
    internal class SearchViewModel : BaseViewModel
    {
        public AsyncCommand SearchCityCommand { get; }
        IInternetWeather internetWeatherService;
        public AsyncCommand SelectedCommand { get; }
        List<Models.Location> _cites;
        public List<Models.Location> Cites
        {
            get => _cites;
            set => SetProperty(ref _cites, value);
        }

        string _city;
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }
        public SearchViewModel()
        {
            Title = "Search";
            SearchCityCommand = new AsyncCommand(SearchCity);
            internetWeatherService = DependencyService.Get<IInternetWeather>();
            SelectedCommand = new AsyncCommand(SelectedAsync);
        }

        Models.Location selectedCity;
        public Models.Location SelectedCity
        {
            get => selectedCity;
            set => SetProperty(ref selectedCity, value);
        }
       private async Task SelectedAsync()
        {
            
            if (SelectedCity == null)
                return;
            Preferences.Set("city",selectedCity.Name);
            MessagingCenter.Send<object>(this, "ChangeTabHome");

            MessagingCenter.Send(this, "DataMessage", SelectedCity);

        }
        async Task SearchCity()
        {

            IsBusy = true;
            Cites = await internetWeatherService.GetSearchCityWeather(City);
            IsBusy = false;
        }

    }
}
