using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;
using Condition = WeatherApp.Models.Condition;

[assembly: Dependency(typeof(CurrentWeatherService))]
namespace WeatherApp.Services
{

    public class CurrentWeatherService : ICurrentWeather
    {
        SQLiteAsyncConnection db;
        async Task Init()
        {
            if (db != null) return;
            // Get an absolute path to the database file


            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbPath = Path.Combine(folderPath, "WeatherDB.db");

            // Check if the Documents directory exists, and create it if necessary
            string documentsPath = Path.Combine(folderPath, "Documents");
            if (!Directory.Exists(documentsPath))
            {
                Directory.CreateDirectory(documentsPath);
            }

            // Combine the Documents directory with the database file name
            dbPath = Path.Combine(documentsPath, "WeatherDB.db");

            db = new SQLiteAsyncConnection(dbPath);


            try
            {
                await db.CreateTableAsync<Condition>();
                await db.CreateTableAsync<Current>();
                await db.CreateTableAsync<Models.Location>();
             
                await db.CreateTableAsync<CurrentWeather>();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"Error creating table: {ex.Message}");
            }
        }
        public async Task<int> AddCurrentWeather(CurrentWeather currentWeather)
        {
            await Init();
           

            var id = await db.InsertAsync(currentWeather);
            return id;
        }

        public async Task<CurrentWeather> GetCurrentWeather()
        {
            await Init();

            
            var currentWeather = await db.Table<CurrentWeather>()
   .OrderByDescending(c => c.Id)
   .FirstOrDefaultAsync();
            return currentWeather;
        }

        public async Task RemoveCurrentWeather(int id)
        {
            await Init();

            await db.DeleteAsync<CurrentWeather>(id);

        }

        public async Task<int> AddCurrent(Current current)
        {
            await Init();


            var id = await db.InsertAsync(current);
            return id;
        }

        public async Task<int> AddLocation(Models.Location location)
        {
            await Init();


            var id = await db.InsertAsync(location);
            return id;
        }

        public async Task<int> AddCondition(Condition condition)
        {
            await Init();


            var id = await db.InsertAsync(condition);
            return id;
        }

        public async Task<Current> GetCurrent(int id)
        {
            await Init();

            var current = await db.Table<Current>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return current;
        }

        public async Task<Models.Location> GetLocation(int id)
        {
            await Init();
            var location = await db.Table<Models.Location>()
               .FirstOrDefaultAsync(c => c.Id == id);

            return location;
        }

        public async Task<Condition> GetCondition(int id)
        {
            await Init();

            var condition = await db.Table<Condition>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return condition;
        }
 
    }
}
