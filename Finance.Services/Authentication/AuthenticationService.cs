using Finance.Core;
using Finance.Services.Authentication.Interface;

namespace Finance.Services.Authentication
{
    public class AuthenticationService : ObservableObject, IAuthenticationService
    {
        private string _user;
        public string User
        {
            get { return _user; }
            set
            { 
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private bool _isAuthenticated;

        public bool IsAuthenticated()
        {
            return _isAuthenticated;
        }

        public void SetAuthentication(bool authenticate)
        {
            _isAuthenticated = authenticate;
        }
    }
}
