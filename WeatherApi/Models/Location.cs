namespace WeatherApi.Models
{
    public class Location
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string TzId { get; set; }
        public long LocaltimeEpoch { get; set; }
        public string Localtime { get; set; }
    }
}
