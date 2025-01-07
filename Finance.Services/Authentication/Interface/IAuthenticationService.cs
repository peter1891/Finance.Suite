namespace Finance.Services.Authentication.Interface
{
    public interface IAuthenticationService
    {
        string User {  get; }
        string Email { get; }

        void SetAuthentication(bool authenticate);
        bool IsAuthenticated();
    }
}
