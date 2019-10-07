using OpenWeatherStefanini.Models;
using OpenWeatherStefanini.Services;
using OpenWeatherStefanini.Utils;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
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

        private readonly RestService _restService;

        private readonly PageDialogService _pageDialogService;

        private bool nofavoriteCities;
        public bool NofavoriteCities {
            get { return nofavoriteCities; }
            set { SetProperty(ref nofavoriteCities, value); }
        }

        private ObservableCollection<City> cities;
        public ObservableCollection<City> Cities {
            get { return cities; }
            private set { SetProperty(ref cities, value); }
        }

        public MainPageViewModel(INavigationService navigationService, ResourceDataService resourceDataService,
            FavoriteCityService favoriteCityService, RestService restService, PageDialogService pageDialogService)
            : base(navigationService)
        {
            GetFavoriteCitiesAndWeatherCommand = new DelegateCommand(async () => await GetFavoriteCitiesAndWeather());
            CitiesPageCommand = new DelegateCommand(() => NavigationService.NavigateAsync(PageName.CitiesPage));
            ItemTappedCommand = new DelegateCommand<City>(ShowCityDetails);
            _resourceDataService = resourceDataService;
            _favoriteCityService = favoriteCityService;
            _restService = restService;
            _pageDialogService = pageDialogService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            GetFavoriteCitiesAndWeatherCommand.Execute();
        }

        private async Task GetFavoriteCitiesAndWeather()
        {
            var allCities = _resourceDataService.GetCities();
            var favoriteKeys = await _favoriteCityService.GetFavoriteCitiesAsync();
            IsBusy = true;

            if (favoriteKeys.Any())
            {
                NofavoriteCities = false;
                var favoriteCities = allCities.Where(c => favoriteKeys.Select(f => f.Key).Contains(c.Id));
                var tasks = new List<Task>();
                foreach (var item in favoriteCities)
                {
                    tasks.Add(Task.Run(async () => item.Weather = await _restService.GetWeather(item.Id)));
                }

                var continuation = Task.WhenAll(tasks);

                try
                {
                    continuation.Wait();
                    Cities = new ObservableCollection<City>(favoriteCities);
                } catch (Exception)
                {
                    await _pageDialogService.DisplayAlertAsync(Message.MessageText, Message.FetchWeatherDataFailed, Message.Ok);
                } finally
                {
                    IsBusy = false;
                }
            } else
            {
                NofavoriteCities = true;
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
