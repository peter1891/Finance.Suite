namespace Finance.Services.Authentication.Interface
{
    public interface IAuthenticationService
    {
        string User {  get; }
        string Email { get; }
        bool IsAuthenticated { get; }

        void Login(Tuple<string, string> model);
        void Logout();
    }
}
