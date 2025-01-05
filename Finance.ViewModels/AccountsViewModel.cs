using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Navigation.Interface;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class AccountsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IAccountRepository _accountRepository;

        public ObservableCollection<AccountModel> AccountModels { get; set; } = new ObservableCollection<AccountModel>();

        public ICommand NavigateAddCommand { get; set; }

        public AccountsViewModel(INavigationService navigationService, IAccountRepository accountRepository)
        {
            _navigationService = navigationService;
            _accountRepository = accountRepository;

            GetAccountsAsync();

            NavigateAddCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("account"); }, obj => true);
        }

        private async void GetAccountsAsync()
        {
            if (AccountModels.Count != 0)
                AccountModels.Clear();

            var accountModels = await _accountRepository.GetAllAsync();
            foreach (AccountModel accountModel in accountModels)
                AccountModels.Add(accountModel);
        }
    }
}
