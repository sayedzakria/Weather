using SQLite;

namespace WeatherApp.Models
{
    public class CurrentWeather
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int CurrentId { get; set; }
        [Ignore]
        public Location Location { get; set; }
        [Ignore]
        public Current Current { get; set; }
    }
}
