using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;
using Xamarin.Essentials;

namespace WeatherApp.Services
{
    public class InternetWeather : IInternetWeather
    {
        static string BaseUrl = "http://10.0.2.2:7177"; // Use 10.0.2.2 for Android emulator

        static HttpClient client;

        public InternetWeather()
        {
            try
            {
                var handler = new HttpClientHandler();

                // Ignore SSL certificate validation errors (for testing purposes)
                handler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(BaseUrl)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle exceptions
            }
        }

        public async Task<CurrentWeather> GetCurrentWeather(string city, string lang)
        {
            var json = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync($"/WeatherForecast/GetCurrent?city={city}&lang={lang}");
                }

                // Deserialize JSON response
                return JsonConvert.DeserializeObject<CurrentWeather>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle exceptions
                throw ex;
            }
        }

        public async Task<List<Models.Location>> GetSearchCityWeather(string city)
        {
            var json = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync($"/WeatherForecast/GetSearchCity?city={city}");
                }

                // Deserialize JSON response
                return JsonConvert.DeserializeObject< List<Models.Location>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle exceptions
                throw ex;
            }
        }


       public  async Task<AllForcast> GetForcastWeather(string city, string dayes)
        {
            var json = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync($"/WeatherForecast/GetForcast?city={city}&days={dayes}");
                }

                // Deserialize JSON response
                return JsonConvert.DeserializeObject<AllForcast>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle exceptions
                throw ex;
            }
        }

     
    }
}
