using SQLite;

namespace WeatherApp.Models
{
   
    public class Current
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public long LastUpdatedEpoch { get; set; }
        public string LastUpdated { get; set; }
        public decimal TempC { get; set; }
        public decimal TempF { get; set; }
        public int IsDay { get; set; }
        [Ignore]
        public Condition Condition { get; set; }
        public int ConditionId { get; set; }
        public decimal WindMph { get; set; }
        public decimal WindKph { get; set; }
        public int WindDegree { get; set; }
        public string WindDir { get; set; }
        public decimal PressureMb { get; set; }
        public decimal PressureIn { get; set; }
        public decimal PrecipMm { get; set; }
        public decimal PrecipIn { get; set; }
        public int Humidity { get; set; }
        public int Cloud { get; set; }
        public decimal FeelslikeC { get; set; }
        public decimal FeelslikeF { get; set; }
        public decimal VisKm { get; set; }
        public decimal VisMiles { get; set; }
        public decimal Uv { get; set; }
        public decimal GustMph { get; set; }
        public decimal GustKph { get; set; }
    }
}
