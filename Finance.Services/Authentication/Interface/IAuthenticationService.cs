using Finance.Models;

namespace Finance.Services.Authentication.Interface
{
    public interface IAuthenticationService
    {
        UserModel UserModel {  get; }
        bool IsAuthenticated { get; }

        void Login(UserModel userModel);
        void Logout();
    }
}
