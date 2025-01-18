using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Utilities.Database;
using Microsoft.EntityFrameworkCore;

namespace Finance.Repository.Repository.Models
{
    public class TransactionRepository : Repository<TransactionModel>, ITransactionRepository
    {
        public TransactionRepository(DatabaseContext databaseContext)
            : base(databaseContext) 
        {

        }

        public async Task AddTransactionsAsync(IEnumerable<TransactionModel> transactionModels)
        {
            await DatabaseContext.AddRangeAsync(transactionModels);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task<bool> VerifyTransactionAsync(TransactionModel transactionModel)
        {
            if (await DatabaseContext.TransactionModels.AnyAsync(t => t.AccountId == transactionModel.AccountId && t.Description == transactionModel.Description))
                return false;

            return true;
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }
    }
}
