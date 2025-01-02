using Finance.Core;
using Finance.Services.Navigation.Interface;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class MainViewModel : ViewModelBase
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

        public ICommand NavigateAccountsCommand { get; set; }
        public ICommand NavigateTransactionsCommand { get; set; }

        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigationService.NavigateTo<TransactionsViewModel>();

            NavigateAccountsCommand = new RelayCommand(obj => { _navigationService.NavigateTo<AccountsViewModel>(); }, obj => true);
            NavigateTransactionsCommand = new RelayCommand(obj => { _navigationService.NavigateTo<TransactionsViewModel>(); }, obj => true);
        }
    }
}
