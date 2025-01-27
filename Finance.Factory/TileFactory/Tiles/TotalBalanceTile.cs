using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;

namespace Finance.Factory.TileFactory.Tiles
{
    public class TotalBalanceTile : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITransactionRepository _transactionRepository;

        public double TotalBalance { get; set; }

        public TotalBalanceTile(
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
            List<TransactionModel> transactionModels = new List<TransactionModel>(await _transactionRepository.GetTransactionsByAuthenticatedIdAsync(_authenticationService.UserId));

            foreach (TransactionModel transactionModel in transactionModels)
            {
                if (transactionModel.Type == Enums.TransactionType.Credit)
                    TotalBalance += transactionModel.Amount;
                else
                    TotalBalance -= transactionModel.Amount;
            }
        }
    }
}
