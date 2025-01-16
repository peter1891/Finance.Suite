using Finance.Models;

namespace Finance.Services.Authentication.Interface
{
    public interface IAuthenticationService
    {
        int UserId {  get; }
        bool IsAuthenticated { get; }

        void Login(int userId, string firstName);
        void Logout();
    }
}
