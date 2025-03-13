using Finance.Core;
using Finance.Services.Authentication;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using Firebase.Auth;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;

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

        public MainViewModel(
            FirebaseAuthClient firebaseAuthClient,
            IAuthenticationService authenticationService,
            INavigationService navigationService)
        {
            _firebaseAuthClient = firebaseAuthClient;

            AuthenticationService = authenticationService;
            NavigationService = navigationService;
            NavigationService.NavigateTo<DashboardViewModel>();

            NavigateAccountsCommand = new RelayCommand(obj => { _navigationService.NavigateTo<AccountsViewModel>(); }, obj => true);
            NavigateAllocationsCommand = new RelayCommand(obj => { _navigationService.NavigateTo<AllocationsViewModel>(); }, obj => true);
            NavigateDashboardCommand = new RelayCommand(obj => { _navigationService.NavigateTo<DashboardViewModel>(); }, obj => true);
            NavigateTransactionsCommand = new RelayCommand(obj => { _navigationService.NavigateTo<TransactionsViewModel>(); }, obj => true);

            ExecuteLogoutCommand = new RelayCommand(ExecuteLogout, obj => true);
        }

        private void ExecuteLogout(object obj)
        {
            if (AuthenticationService.IsAuthenticated)
            {
                AuthenticationService.Logout();
                _firebaseAuthClient.SignOut();
            }
            else
                _navigationService.NavigateTo<LoginViewModel>();
        }
    }
}
