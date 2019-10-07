using OpenWeatherStefanini.Models;
using OpenWeatherStefanini.Services;
using OpenWeatherStefanini.Utils;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace OpenWeatherStefanini.ViewModels
{
    public class CitiesPageViewModel : ViewModelBase
    {
        public DelegateCommand<City> ItemTappedCommand { get; private set; }

        public readonly ResourceDataService _resourceDataService;

        private ObservableCollection<City> cities;
        public ObservableCollection<City> Cities {
            get { return cities; }
            private set { SetProperty(ref cities, value); }
        }

        public CitiesPageViewModel(INavigationService navigationService, ResourceDataService resourceDataService)
            : base(navigationService)
        {
            ItemTappedCommand = new DelegateCommand<City>(ShowCityDetails);
            _resourceDataService = resourceDataService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadCities();
        }

        private void ShowCityDetails(City selectedCity)
        {
            var parameters = new NavigationParameters
            {
                { NavigationParamKey.SelectedCity, selectedCity }
            };
            NavigationService.NavigateAsync(PageName.CityDetailsPage, parameters);
        }

        private void LoadCities()
        {
            IsBusy = true;
            try
            {
                Cities = new ObservableCollection<City>(_resourceDataService.GetCities());
            } finally
            {
                IsBusy = false;
            }
        }
    }
}
