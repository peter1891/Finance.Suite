using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;
using System.Transactions;

namespace Finance.ViewModels.Tiles
{
    public class ExpensesTileViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITransactionRepository _transactionRepository;

        public List<TransactionModel> TransactionModels { get; set; }

        public string Expenses { get; set; }

        public ExpensesTileViewModel(
            IAuthenticationService authenticationService,
            ITransactionRepository transactionRepository
            )
        {
            _authenticationService = authenticationService;
            _transactionRepository = transactionRepository;

            GetExpenses();
        }

        private async void GetExpenses()
        {
            var expensesList = await _transactionRepository.GetDebitTransactionsByAuthIdAsync(
                _authenticationService.UserId,
                new DateTime(2024, 10, 1),
                new DateTime(2024, 12, 31));

            TransactionModels = expensesList.Take(3).ToList();

            double expenses = Math.Round(expensesList.Sum(t => t.Amount), 2);

            Expenses = String.Format("{0:0.00}", expenses);
        }
    }
}
