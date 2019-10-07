using OpenWeatherStefanini.Models;
using OpenWeatherStefanini.Services;
using OpenWeatherStefanini.Utils;
using Prism.Commands;
using Prism.Navigation;

namespace OpenWeatherStefanini.ViewModels
{
    public class CityDetailsPageViewModel : ViewModelBase
    {
        public DelegateCommand AddOrRemoveFavoriteCityCommand { get; private set; }

        private readonly FavoriteCityService _cityRegistrationService;

        private City city;
        public City City {
            get { return city; }
            set { SetProperty(ref city, value); }
        }

        public CityDetailsPageViewModel(INavigationService navigationService, FavoriteCityService cityRegistrationService)
            : base(navigationService)
        {
            _cityRegistrationService = cityRegistrationService;
            AddOrRemoveFavoriteCityCommand = new DelegateCommand(AddOrRemoveFavoriteCity);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var selectedCity = parameters.GetValue<City>(NavigationParamKey.SelectedCity);
            var favorited = await _cityRegistrationService.GetFavoriteCityAsync(selectedCity.Id);
            selectedCity.Favorited = favorited != null;
            City = selectedCity;
        }

        private void AddOrRemoveFavoriteCity()
        {
            City.Favorited = !City.Favorited;
            var favoriteCity = new FavoriteCity(City.Id);
            if (City.Favorited)
            {
                _cityRegistrationService.SaveFavoriteCityAsync(favoriteCity);
            } else
            {
                _cityRegistrationService.DeleteFavoriteCityAsync(favoriteCity);
            }
            RaisePropertyChanged("City");
        }
    }
}
