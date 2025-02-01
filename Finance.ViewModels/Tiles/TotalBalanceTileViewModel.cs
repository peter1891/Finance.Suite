using Finance.Core;
using Finance.Enums;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;

namespace Finance.ViewModels.Tiles
{
    public class TotalBalanceTileViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITransactionRepository _transactionRepository;

        public string TotalBalance { get; set; }

        public TotalBalanceTileViewModel(
            IAuthenticationService authenticationService,
            ITransactionRepository transactionRepository
            )
        {
            _authenticationService = authenticationService;
            _transactionRepository = transactionRepository;

            GetTotalBalance();
        }

        private async void GetTotalBalance()
        {
            var balanceList = new List<TransactionModel>(await _transactionRepository.GetTransactionsByAuthIdAsync(_authenticationService.UserId));

            double totalBalance = Math.Round(
                balanceList.Where(t => t.Type == TransactionType.Credit).Sum(t => t.Amount) - 
                balanceList.Where(t => t.Type == TransactionType.Debit).Sum(t => t.Amount), 2);

            TotalBalance = String.Format("{0:0.00}", totalBalance);
        }
    }
}
