using OpenWeatherStefanini.Models;
using OpenWeatherStefanini.Services;
using OpenWeatherStefanini.Utils;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;

namespace OpenWeatherStefanini.ViewModels
{
    public class CityDetailsPageViewModel : ViewModelBase
    {
        public DelegateCommand AddOrRemoveFavoriteCityCommand { get; private set; }

        private readonly FavoriteCityService _cityRegistrationService;

        private readonly RestService _restService;

        private readonly PageDialogService _pageDialogService;

        private City city;
        public City City {
            get { return city; }
            set { SetProperty(ref city, value); }
        }

        public CityDetailsPageViewModel(INavigationService navigationService, FavoriteCityService cityRegistrationService,
            RestService restService, PageDialogService pageDialogService)
            : base(navigationService)
        {
            City = new City();
            _cityRegistrationService = cityRegistrationService;
            _restService = restService;
            _pageDialogService = pageDialogService;
            AddOrRemoveFavoriteCityCommand = new DelegateCommand(AddOrRemoveFavoriteCity);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var selectedCity = parameters.GetValue<City>(NavigationParamKey.SelectedCity);
            await LoadCity(selectedCity);
        }

        private async Task LoadCity(City selectedCity)
        {
            var favorited = await _cityRegistrationService.GetFavoriteCityAsync(selectedCity.Id);
            selectedCity.Favorited = favorited != null;

            IsBusy = true;
            try
            {
                selectedCity.Weather = await _restService.GetWeather(selectedCity.Id);
                City = selectedCity;
            } catch (Exception)
            {
                await _pageDialogService.DisplayAlertAsync(Message.MessageText, Message.FetchWeatherDataFailed, Message.Ok);
            } finally
            {
                IsBusy = false;
            }
        }

        private void AddOrRemoveFavoriteCity()
        {
            if (!IsBusy)
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
}
