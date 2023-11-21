using SQLite;

namespace SharedWeather.Models
{
    public class CurrentWeather
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public Location Location { get; set; }
        public Current Current { get; set; }
    }
}
