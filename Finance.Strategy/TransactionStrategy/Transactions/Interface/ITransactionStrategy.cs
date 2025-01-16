using Finance.Models;

namespace Finance.Strategy.TransactionStrategy.Transactions.Interface
{
    public interface ITransactionStrategy
    {
        void ImportTransactions(int accountId, string file);
    }
}
