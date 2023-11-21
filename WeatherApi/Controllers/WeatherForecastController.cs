using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using WeatherApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly string _apiKey;

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration["ApiKey"];
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //Get Current city weather
        [HttpGet( "GetCurrent")]
        public async Task<IActionResult> GetCurrentAsync(string city, string lang)
        {
            string apiUrl = $"https://api.weatherapi.com/v1/current.json?q={city}&lang={lang}&key=" + _apiKey;

            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                   

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    CurrentWeather currentWeather = new CurrentWeather();
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();
                        currentWeather = JsonConvert.DeserializeObject<CurrentWeather>(jsonResult) ?? new CurrentWeather();
                        return StatusCode(200, currentWeather);
                    }
                    else
                    {
                        // Log the response details for debugging purposes
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error Response: {errorMessage}");
                        return StatusCode((int)response.StatusCode, $"Error: {response.ReasonPhrase}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    return StatusCode(500, $"Error: {ex.Message}");
                }
            }
        }
        //Get Search citys weather
        [HttpGet("GetSearchCity")]
        public async Task<IActionResult> GetSearchCityAsync(string city)
        {
            string apiUrl = $"https://api.weatherapi.com/v1/search.json?q={city}&key=" + _apiKey;

            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    List<Location> cites = new List<Location>();
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();
                        cites = JsonConvert.DeserializeObject<List<Location>>(jsonResult) ?? new List<Location>();
                        return StatusCode(200, cites);
                    }
                    else
                    {
                        // Log the response details for debugging purposes
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error Response: {errorMessage}");
                        return StatusCode((int)response.StatusCode, $"Error: {response.ReasonPhrase}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    return StatusCode(500, $"Error: {ex.Message}");
                }
            }
        }

        //Get Forcast weather
        [HttpGet("GetForcast")]
        public async Task<IActionResult> GetForcast(string city,string days)
        {
            string apiUrl = $"https://api.weatherapi.com/v1/forecast.json?q={city}&days={days}&key=" + _apiKey;

            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    AllForcast allForcast = new AllForcast();
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();
                        allForcast = JsonConvert.DeserializeObject<AllForcast>(jsonResult) ?? new AllForcast();
                        return StatusCode(200, allForcast);
                    }
                    else
                    {
                        // Log the response details for debugging purposes
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error Response: {errorMessage}");
                        return StatusCode((int)response.StatusCode, $"Error: {response.ReasonPhrase}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    return StatusCode(500, $"Error: {ex.Message}");
                }
            }
        }
    }
}
