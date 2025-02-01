using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;

namespace Finance.ViewModels.Tiles
{
    public class IncomeTileViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITransactionRepository _transactionRepository;

        public List<TransactionModel> TransactionModels { get; set; }

        public string Income { get; set; }

        public IncomeTileViewModel(
            IAuthenticationService authenticationService,
            ITransactionRepository transactionRepository
            )
        {
            _authenticationService = authenticationService;
            _transactionRepository = transactionRepository;

            GetIncome();
        }

        private async void GetIncome()
        {
            var incomeList = await _transactionRepository.GetCreditTransactionsByAuthIdAsync(
                _authenticationService.UserId,
                new DateTime(2024, 10, 1),
                new DateTime(2024, 12, 31));

            TransactionModels = incomeList.Take(3).ToList();

            double income = Math.Round(incomeList.Sum(t => t.Amount), 2);

            Income = String.Format("{0:0.00}", income);
        }
    }
}
