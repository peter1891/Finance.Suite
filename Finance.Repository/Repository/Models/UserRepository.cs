using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Utilities.Database;

namespace Finance.Repository.Repository.Models
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {

        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }
    }
}
