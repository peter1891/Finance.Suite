using Finance.Models;
using System.Net;

namespace Finance.Repository.Interface.Models
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<bool> AuthenticateUserAsync(NetworkCredential networkCredential);
        Task<UserModel> GetUserAsync(NetworkCredential networkCredential);
    }
}
