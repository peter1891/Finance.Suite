using Finance.Core;
using Finance.Services.Authentication;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IAuthenticationService _authenticationService;
        public IAuthenticationService AuthenticationService
        {
            get => _authenticationService;
            set
            {
                _authenticationService = value;
                OnPropertyChanged(nameof(AuthenticationService));

                Validate(nameof(AuthenticationService), value, (RelayCommand)ExecuteLogoutCommand);
            }
        }

        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged(nameof(NavigationService));
            }
        }

        public ICommand NavigateAccountsCommand { get; }
        public ICommand NavigateAllocationsCommand { get; }
        public ICommand NavigateDashboardCommand { get; }
        public ICommand NavigateTransactionsCommand { get; }

        public ICommand ExecuteLogoutCommand { get; }

        public MainViewModel(IAuthenticationService authenticationService, INavigationService navigationService)
        {
            AuthenticationService = authenticationService;
            NavigationService = navigationService;
            NavigationService.NavigateTo<DashboardViewModel>();

            NavigateAccountsCommand = new RelayCommand(obj => { _navigationService.NavigateTo<AccountsViewModel>(); }, obj => true);
            NavigateAllocationsCommand = new RelayCommand(obj => { _navigationService.NavigateTo<AllocationsViewModel>(); }, obj => true);
            NavigateDashboardCommand = new RelayCommand(obj => { _navigationService.NavigateTo<DashboardViewModel>(); }, obj => true);
            NavigateTransactionsCommand = new RelayCommand(obj => { _navigationService.NavigateTo<TransactionsViewModel>(); }, obj => true);

            ExecuteLogoutCommand = new RelayCommand(ExecuteLogout, obj => true);
        }

        private bool CanExecuteLogout(object obj)
        {
            return AuthenticationService.IsAuthenticated;
        }

        private void ExecuteLogout(object obj)
        {
            if (AuthenticationService.IsAuthenticated)
                AuthenticationService.Logout();
            else
                _navigationService.NavigateTo<LoginViewModel>();
        }
    }
}
