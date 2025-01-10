using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class AccountsViewModel : ViewModelBase
    {
        private IAuthenticationService _authenticationService;
        public IAuthenticationService AuthenticationService
        {
            get { return _authenticationService; }
            set
            {
                _authenticationService = value;
                OnPropertyChanged(nameof(AuthenticationService));
            }
        }

        private readonly INavigationService _navigationService;
        private readonly IAccountRepository _accountRepository;

        public ICommand NavigateAddCommand { get; }

        public AccountsViewModel(
            IAuthenticationService authenticationService, 
            INavigationService navigationService, 
            IAccountRepository accountRepository)
        {
            AuthenticationService = authenticationService;
            _navigationService = navigationService;
            _accountRepository = accountRepository;

            NavigateAddCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("account"); }, obj => true);
        }
    }
}
