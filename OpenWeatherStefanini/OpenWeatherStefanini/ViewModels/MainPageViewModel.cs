using OpenWeatherStefanini.Models;
using OpenWeatherStefanini.Services;
using OpenWeatherStefanini.Utils;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherStefanini.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand GetFavoriteCitiesAndWeatherCommand { get; private set; }

        public DelegateCommand CitiesPageCommand { get; private set; }

        public DelegateCommand<City> ItemTappedCommand { get; private set; }

        private readonly ResourceDataService _resourceDataService;

        private readonly FavoriteCityService _favoriteCityService;

        private ObservableCollection<City> cities;
        public ObservableCollection<City> Cities {
            get { return cities; }
            private set { SetProperty(ref cities, value); }
        }

        public MainPageViewModel(INavigationService navigationService, ResourceDataService resourceDataService, 
            FavoriteCityService favoriteCityService)
            : base(navigationService)
        {
            GetFavoriteCitiesAndWeatherCommand = new DelegateCommand(async () => await GetFavoriteCitiesAndWeather());
            CitiesPageCommand = new DelegateCommand(() => NavigationService.NavigateAsync(PageName.CitiesPage));
            ItemTappedCommand = new DelegateCommand<City>(ShowCityDetails);
            _resourceDataService = resourceDataService;
            _favoriteCityService = favoriteCityService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            GetFavoriteCitiesAndWeatherCommand.Execute();
        }

        private async Task GetFavoriteCitiesAndWeather()
        {
            IsBusy = true;
            try
            {
                var allCities = _resourceDataService.GetCities();
                var favoriteKeys = await _favoriteCityService.GetFavoriteCitiesAsync();
                Cities = new ObservableCollection<City>(allCities.Where(c => favoriteKeys.Select(f => f.Key).Contains(c.Id)));
            } finally
            {
                IsBusy = false;
            }
        }

        private void ShowCityDetails(City selectedCity)
        {
            var parameters = new NavigationParameters
            {
                { NavigationParamKey.SelectedCity, selectedCity }
            };
            NavigationService.NavigateAsync(PageName.CityDetailsPage, parameters);
        }
    }
}
