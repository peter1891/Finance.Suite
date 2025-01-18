using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using Finance.Strategy.TransactionStrategy;
using Finance.Strategy.TransactionStrategy.Transactions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class AccountsViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;

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

        private readonly TransactionContext _transactionContext;

        public ObservableCollection<AccountModel> AccountModels { get; set; } = new ObservableCollection<AccountModel>();

        public ICommand NavigateAddCommand { get; }
        public ICommand NavigateEditCommand { get; }
        public ICommand NavigateImportCommand { get; }

        public AccountsViewModel(
            IServiceProvider serviceProvider,
            IAuthenticationService authenticationService, 
            INavigationService navigationService, 
            IAccountRepository accountRepository,
            TransactionContext transactionContext)
        {
            _serviceProvider = serviceProvider;

            AuthenticationService = authenticationService;
            _navigationService = navigationService;
            _accountRepository = accountRepository;

            _transactionContext = transactionContext;

            GetAccountsAsync();

            NavigateAddCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("account"); }, obj => true);
            NavigateEditCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("account", (int)obj); }, obj => true);
            NavigateImportCommand = new RelayCommand(Temp, obj => true);
        }

        private void Temp(object obj)
        {
            ReadTransactions((int)obj);
        }

        private async void GetAccountsAsync()
        {
            AccountModels = new ObservableCollection<AccountModel>(await _accountRepository.GetAccountsByAuthenticatedIdAsync(AuthenticationService.UserId));
        }

        private void ReadTransactions(int accountId)
        {
            _transactionContext.SetTransactionContext(_serviceProvider.GetRequiredService<IngTransaction>());
            _transactionContext.ProcessTransaction(accountId, "C:\\Users\\peter\\ing.csv");
        }
    }
}
