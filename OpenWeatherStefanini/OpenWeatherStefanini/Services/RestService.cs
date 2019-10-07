using Newtonsoft.Json.Linq;
using OpenWeatherStefanini.Models;
using OpenWeatherStefanini.Utils;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace OpenWeatherStefanini.Services
{
    public class RestService
    {
        public async Task<Weather> GetWeather(int cityId)
        {
            var builder = new UriBuilder($"{OpenWeatherApiParam.BaseApiUrl}/{OpenWeatherApiParam.Weather}");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Add(OpenWeatherApiParam.Id, cityId.ToString());
            query.Add(OpenWeatherApiParam.AppId, OpenWeatherApiParam.ApiKey);
            query.Add(OpenWeatherApiParam.Lang, OpenWeatherApiParam.LangValue);
            query.Add(OpenWeatherApiParam.Units, OpenWeatherApiParam.UnitVlue);
            builder.Query = query.ToString();

            Weather weather = null;
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(builder.ToString());
                var parsed = JObject.Parse(response);

                weather = new Weather();
                weather.Description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(parsed.GetValue("weather").First.Value<string>("description"));
                weather.Icon = $"{OpenWeatherApiParam.BaseSiteUrl}/{OpenWeatherApiParam.Icon}/{parsed.GetValue("weather").First.Value<string>("icon")}.png";
                weather.Temp = $"{parsed.GetValue("main").Value<int>("temp")}c";
                weather.TempMin = $"{parsed.GetValue("main").Value<int>("temp_min")}c";
                weather.TempMax = $"{parsed.GetValue("main").Value<int>("temp_max")}c";
            }
            return weather;
        }
    }
}
