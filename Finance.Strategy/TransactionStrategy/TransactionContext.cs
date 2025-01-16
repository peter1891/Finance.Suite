using Finance.Models;
using Finance.Strategy.TransactionStrategy.Transactions.Interface;

namespace Finance.Strategy.TransactionStrategy
{
    public class TransactionContext
    {
        private ITransactionStrategy _transactionStrategy;

        public void SetTransactionContext(ITransactionStrategy transactionStrategy)
        {
            _transactionStrategy = transactionStrategy;
        }

        public void ProcessTransaction(int accountId, string file)
        {
            _transactionStrategy.ImportTransactions(accountId, file);
        }
    }
}
