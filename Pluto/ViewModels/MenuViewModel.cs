using Finance.Core;
using Finance.Services.Navigation.Interface;
using Finance.ViewModels;
using System.Windows.Input;

namespace Pluto.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get { return _navigationService; }
            set
            {
                _navigationService = value;
                OnPropertyChanged(nameof(NavigationService));
            }
        }

        public ICommand NavigateTransactionsCommand { get; set; }

        public MenuViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigationService.NavigateTo<TransactionsViewModel>();

            NavigateTransactionsCommand = new RelayCommand(obj => { _navigationService.NavigateTo<TransactionsViewModel>(); }, obj => true);
        }
    }
}
