﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class Day
    {
        public double maxtemp_c { get; set; }
        public double maxtemp_f { get; set; }
        public double mintemp_c { get; set; }
        public double mintemp_f { get; set; }
        public double avgtemp_c { get; set; }
        public double avgtemp_f { get; set; }
        public double maxwind_mph { get; set; }
        public double maxwind_kph { get; set; }
        public double totalprecip_mm { get; set; }
        public double totalprecip_in { get; set; }
        public double totalsnow_cm { get; set; }
        public double avgvis_km { get; set; }
        public double avgvis_miles { get; set; }
        public double avghumidity { get; set; }
        public double daily_will_it_rain { get; set; }
        public double daily_chance_of_rain { get; set; }
        public double daily_will_it_snow { get; set; }
        public double daily_chance_of_snow { get; set; }
        public Condition condition { get; set; }
        public double uv { get; set; }
        public Color BGColor { get; set; } = Color.FromHex("#696589");
    }
}
