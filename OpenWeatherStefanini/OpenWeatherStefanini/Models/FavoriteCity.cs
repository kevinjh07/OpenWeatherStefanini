using SQLite;

namespace OpenWeatherStefanini.Models
{
    public class FavoriteCity
    {
        [PrimaryKey]
        public int Key { get; set; }

        public FavoriteCity(int key)
        {
            Key = key;
        }

        public FavoriteCity()
        {

        }
    }
}
