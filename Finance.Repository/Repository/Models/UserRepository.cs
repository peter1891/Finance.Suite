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

        public async Task<Tuple<string, string>> GetUserAsync(NetworkCredential networkCredential)
        {
            UserModel userModel = await DatabaseContext.UserModels
                .FirstOrDefaultAsync(u => u.Email == networkCredential.UserName && u.Password == networkCredential.Password);

            return new Tuple<string, string>(userModel.Name, userModel.Email);
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }
    }
}
