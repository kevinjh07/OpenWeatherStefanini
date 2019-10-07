using Newtonsoft.Json.Linq;
using OpenWeatherStefanini.Models;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace OpenWeatherStefanini.ViewModels
{
    public class CitiesPageViewModel : ViewModelBase
    {
        private ObservableCollection<City> cities;
        public ObservableCollection<City> Cities {
            get { return cities; }
            private set { SetProperty(ref cities, value); }
        }

        public CitiesPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            GetJsonData();
        }

        private void GetJsonData()
        {
            IsBusy = true;
            try
            {
                var assembly = typeof(App).Assembly;
                var path = $"{assembly.GetName().Name}.Resources.city.list.json";
                var stream = assembly.GetManifestResourceStream(path);

                using (var reader = new StreamReader(stream))
                {
                    var jsonString = reader.ReadToEnd();
                    var data = JObject.Parse(jsonString).Value<JToken>("data");
                    var citiesData = data.ToObject<List<City>>();
                    Cities = new ObservableCollection<City>(citiesData);
                }
            } finally
            {
                IsBusy = false;
            }
        }
    }
}
