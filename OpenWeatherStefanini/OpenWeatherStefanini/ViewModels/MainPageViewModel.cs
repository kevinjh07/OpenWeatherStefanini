using OpenWeatherStefanini.Utils;
using Prism.Commands;
using Prism.Navigation;

namespace OpenWeatherStefanini.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand CitiesPageCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            CitiesPageCommand = new DelegateCommand(() => NavigationService.NavigateAsync(PageName.CitiesPage));
        }
    }
}
