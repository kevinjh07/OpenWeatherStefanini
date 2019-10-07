using OpenWeatherStefanini.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OpenWeatherStefanini.Services
{
    public class FavoriteCityService
    {
        private readonly SQLiteAsyncConnection _database;

        private readonly string _databasePath = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "FavoriteCities.db3");

        public FavoriteCityService()
        {
            _database = new SQLiteAsyncConnection(_databasePath);
            _database.CreateTableAsync<FavoriteCity>();
        }

        public Task<List<FavoriteCity>> GetFavoriteCitiesAsync() => _database.Table<FavoriteCity>().ToListAsync();

        public Task<FavoriteCity> GetFavoriteCityAsync(int id) => _database.Table<FavoriteCity>().Where(i => i.Key == id).FirstOrDefaultAsync();

        public Task<int> SaveFavoriteCityAsync(FavoriteCity favoriteCity) => _database.InsertAsync(favoriteCity);

        public Task<int> DeleteFavoriteCityAsync(FavoriteCity favoriteCity) => _database.DeleteAsync(favoriteCity);
    }
}
