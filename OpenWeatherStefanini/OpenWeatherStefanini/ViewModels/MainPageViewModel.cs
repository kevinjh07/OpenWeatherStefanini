using OpenWeatherStefanini.Models;
using OpenWeatherStefanini.Utils;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace OpenWeatherStefanini.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand CitiesPageCommand { get; private set; }

        public DelegateCommand<City> ItemTappedCommand { get; private set; }

        private ObservableCollection<City> cities;
        public ObservableCollection<City> Cities {
            get { return cities; }
            private set { SetProperty(ref cities, value); }
        }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            CitiesPageCommand = new DelegateCommand(() => NavigationService.NavigateAsync(PageName.CitiesPage));
            ItemTappedCommand = new DelegateCommand<City>(ShowCityDetails);
            Cities = new ObservableCollection<City>();
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
