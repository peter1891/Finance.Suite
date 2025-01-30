using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using System.Windows.Documents;
using System.Windows.Input;

namespace Finance.ViewModels.Tiles
{
    public class TransactionsTileViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;
        private readonly ITransactionRepository _transactionRepository;

        public List<TransactionModel> TransactionModels { get; set; }

        public ICommand NavigateTransactionsCommand { get; }

        public TransactionsTileViewModel(
            ITransactionRepository transactionRepository,
            IAuthenticationService authenticationService, 
            INavigationService navigationService
            )
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            _transactionRepository = transactionRepository;

            GetTransactionsAsync();

            NavigateTransactionsCommand = new RelayCommand(obj => { _navigationService.NavigateTo<TransactionsViewModel>(); }, obj => true);
        }

        private async Task GetTransactionsAsync()
        {
            TransactionModels = new List<TransactionModel>(await _transactionRepository.GetTopTransactionsByAuthenticatedIdAsync(_authenticationService.UserId));
        }
    }
}
