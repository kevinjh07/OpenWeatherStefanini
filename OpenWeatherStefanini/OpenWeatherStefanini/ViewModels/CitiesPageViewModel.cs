using Newtonsoft.Json.Linq;
using OpenWeatherStefanini.Models;
using OpenWeatherStefanini.Utils;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace OpenWeatherStefanini.ViewModels
{
    public class CitiesPageViewModel : ViewModelBase
    {
        public DelegateCommand<City> ItemTappedCommand { get; private set; }

        private ObservableCollection<City> cities;
        public ObservableCollection<City> Cities {
            get { return cities; }
            private set { SetProperty(ref cities, value); }
        }

        public CitiesPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ItemTappedCommand = new DelegateCommand<City>(ShowCityDetails);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            GetJsonData();
        }

        private void ShowCityDetails(City selectedCity)
        {
            var parameters = new NavigationParameters
            {
                { NavigationParamKey.SelectedCity, selectedCity }
            };
            NavigationService.NavigateAsync(PageName.CityDetailsPage, parameters);
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
