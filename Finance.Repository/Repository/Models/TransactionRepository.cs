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

        public override async Task<TransactionModel> GetByIdAsync(int id)
        {
            return await DatabaseContext.TransactionModels
                .Include(t => t.Account)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTransactionsAsync(IEnumerable<TransactionModel> transactionModels)
        {
            await DatabaseContext.AddRangeAsync(transactionModels);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task<bool> VerifyTransactionAsync(TransactionModel transactionModel)
        {
            if (await DatabaseContext.TransactionModels
                .AnyAsync(t => t.AccountId == transactionModel.AccountId && t.Description == transactionModel.Description))
                return false;

            return true;
        }

        public async Task<bool> IsRecurringTransactionAsync(TransactionModel transactionModel)
        {
            List<TransactionModel> transactionModels = await DatabaseContext.TransactionModels
                .Where(t => t.CounterParty == transactionModel.CounterParty)
                .ToListAsync();

            if (transactionModels.Count != 0)
            {
                if (transactionModels.Where(t => t.Recurring == true).Count() / transactionModels.Count() * 100 >= 70)
                    return true;
            }

            return false;
        }

        public async Task<IEnumerable<TransactionModel>> GetTransactionsByAuthenticatedIdAsync(int id)
        {
            return await DatabaseContext.TransactionModels
                .Where(t => t.Account.UserId == id)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<TransactionModel>> GetTopTransactionsByAuthenticatedIdAsync(int id)
        {
            return await DatabaseContext.TransactionModels
                .Where(t => t.Account.UserId == id)
                .OrderByDescending(t => t.Date)
                .Take(10)
                .ToListAsync();
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }
    }
}
