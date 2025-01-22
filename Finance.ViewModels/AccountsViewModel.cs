using Finance.Core;
using Finance.Enums;
using Finance.Factory.DialogFactory;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class AccountsViewModel : ViewModelBase
    {        
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        private readonly DialogFactory _dialogFactory;

        private readonly IAccountRepository _accountRepository;

        public ObservableCollection<AccountModel> AccountModels { get; set; } = new ObservableCollection<AccountModel>();

        public ICommand NavigateAddCommand { get; }
        public ICommand NavigateDeleteCommand { get;  }
        public ICommand NavigateEditCommand { get; }
        public ICommand NavigateImportCommand { get; }

        public AccountsViewModel(
            IAuthenticationService authenticationService, 
            INavigationService navigationService, 
            DialogFactory dialogFactory,
            IAccountRepository accountRepository)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;

            _dialogFactory = dialogFactory;

            _accountRepository = accountRepository;

            GetAccountsAsync();

            NavigateAddCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("account"); }, obj => true);
            NavigateDeleteCommand = new RelayCommand(NavigateDelete, obj => true);
            NavigateEditCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("account", (int)obj); }, obj => true);
            NavigateImportCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("importTransaction", (int)obj); }, obj => true);
        }

        private void NavigateDelete(object obj)
        {
            _dialogFactory.SetDialog<AccountsViewModel>(DialogType.RemoveAccount, obj);

            _navigationService.NavigateTo<DialogViewModel>();
        }

        private async void GetAccountsAsync()
        {
            AccountModels = new ObservableCollection<AccountModel>(await _accountRepository.GetAccountsByAuthenticatedIdAsync(_authenticationService.UserId));
        }
    }
}
