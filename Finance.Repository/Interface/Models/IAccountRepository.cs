using Finance.Models;

namespace Finance.Repository.Interface.Models
{
    public interface IAccountRepository : IRepository<AccountModel>
    {
        Task<IEnumerable<AccountModel>> GetAccountsByAuthenticatedIdAsync(int id);
    }
}
