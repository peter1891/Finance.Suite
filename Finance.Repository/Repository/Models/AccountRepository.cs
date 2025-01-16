using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Utilities.Database;
using Microsoft.EntityFrameworkCore;

namespace Finance.Repository.Repository.Models
{
    public class AccountRepository : Repository<AccountModel>, IAccountRepository
    {
        public AccountRepository(DatabaseContext databaseContext)
            : base(databaseContext) 
        {

        }

        public async Task<IEnumerable<AccountModel>> GetAccountsByAuthenticatedIdAsync(int id)
        {
            return await DatabaseContext.AccountModels
                .Where(a => a.UserId == id)
                .ToListAsync();
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }
    }
}
