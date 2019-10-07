using OpenWeatherStefanini.Models;
using OpenWeatherStefanini.Utils;
using Prism.Navigation;

namespace OpenWeatherStefanini.ViewModels
{
    public class CityDetailsPageViewModel : ViewModelBase
    {
        private City city;
        public City City {
            get { return city; }
            set { SetProperty(ref city, value); }
        }

        public CityDetailsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            City = parameters.GetValue<City>(NavigationParamKey.SelectedCity);
        }
    }
}
