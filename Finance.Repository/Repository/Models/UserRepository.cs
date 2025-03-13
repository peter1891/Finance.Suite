using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Utilities.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Finance.Repository.Repository.Models
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {

        }

        public async Task<UserModel> GetUserAsync(string uId)
        {
            return await DatabaseContext.UserModels
                .FirstOrDefaultAsync(u => u.Uid == uId);
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }
    }
}
