namespace WeatherApi.Models
{
    public class AllForcast
    {
        public Location location { get; set; }
        public Current current { get; set; }
        public Forecast forecast { get; set; }
    }
}
