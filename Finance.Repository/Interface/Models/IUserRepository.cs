using Finance.Models;
using System.Net;

namespace Finance.Repository.Interface.Models
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<UserModel> GetUserAsync(string uId);
    }
}
