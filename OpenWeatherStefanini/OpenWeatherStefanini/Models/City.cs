namespace OpenWeatherStefanini.Models
{
    public class City
    {
        public int Id { get; set; }

        public GeoCoordinate Coord { get; set; }

        public string Country { get; set; }

        public string Name { get; set; }

        public int Zoom { get; set; }

        public bool Favorited { get; set; }
    }
}
