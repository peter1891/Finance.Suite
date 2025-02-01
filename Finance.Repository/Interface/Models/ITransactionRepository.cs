using Finance.Models;

namespace Finance.Repository.Interface.Models
{
    public interface ITransactionRepository : IRepository<TransactionModel>
    {
        Task AddTransactionsAsync(IEnumerable<TransactionModel> transactionModels);
        Task<bool> VerifyTransactionAsync(TransactionModel transactionModel);
        Task<bool> IsRecurringTransactionAsync(TransactionModel transactionModel);

        Task<IEnumerable<TransactionModel>> GetTransactionsByAuthIdAsync(int id);
        Task<IEnumerable<TransactionModel>> GetTop10TransactionsByAuthIdAsync(int id);

        Task<IEnumerable<TransactionModel>> GetCreditTransactionsByAuthIdAsync(int id, DateTime from, DateTime to);
        Task<IEnumerable<TransactionModel>> GetDebitTransactionsByAuthIdAsync(int id, DateTime from, DateTime to);
    }
}
