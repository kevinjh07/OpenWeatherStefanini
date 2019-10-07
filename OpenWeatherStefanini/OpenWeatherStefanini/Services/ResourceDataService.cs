using Newtonsoft.Json.Linq;
using OpenWeatherStefanini.Models;
using System.Collections.Generic;
using System.IO;

namespace OpenWeatherStefanini.Services
{
    public class ResourceDataService
    {

        public List<City> GetCities()
        {
            var assembly = typeof(App).Assembly;
            var path = $"{assembly.GetName().Name}.Resources.city.list.json";
            var stream = assembly.GetManifestResourceStream(path);

            List<City> cities = null;
            using (var reader = new StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();
                var data = JObject.Parse(jsonString).Value<JToken>("data");
                cities = data.ToObject<List<City>>();
            }

            return cities;
        }
    }
}
