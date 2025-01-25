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

        public async Task<bool> AuthenticateUserAsync(NetworkCredential networkCredential)
        {
            return await DatabaseContext.UserModels
                .AnyAsync(u => u.Email == networkCredential.UserName && u.Password == networkCredential.Password);
        }

        public async Task<UserModel> GetUserAsync(NetworkCredential networkCredential)
        {
            return await DatabaseContext.UserModels
                .FirstOrDefaultAsync(u => u.Email == networkCredential.UserName && u.Password == networkCredential.Password);
        }

        public async Task<bool> ValidateNewUser(string email)
        {
            return await DatabaseContext.UserModels.AnyAsync(u => u.Email == email);
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }
    }
}
