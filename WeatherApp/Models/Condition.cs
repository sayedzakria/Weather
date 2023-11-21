using SQLite;

namespace WeatherApp.Models
{
   
    public class Condition
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public int Code { get; set; }
    }
}
