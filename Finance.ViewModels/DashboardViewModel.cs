using Finance.Core;
using Finance.Services.Navigation.Interface;

namespace Finance.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public DashboardViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
