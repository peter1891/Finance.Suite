using Finance.Models;

namespace Finance.Repository.Interface.Models
{
    public interface ITransactionRepository : IRepository<TransactionModel>
    {
        Task AddTransactionsAsync(IEnumerable<TransactionModel> transactionModels);
        Task<bool> VerifyTransactionAsync(TransactionModel transactionModel);
        Task<bool> IsRecurringTransactionAsync(TransactionModel transactionModel);

        Task<IEnumerable<TransactionModel>> GetTransactionsByAuthenticatedIdAsync(int id);
    }
}
