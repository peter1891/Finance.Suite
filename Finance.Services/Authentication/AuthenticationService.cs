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
        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set
            {
                _isAuthenticated = value;
                OnPropertyChanged(nameof(IsAuthenticated));
            }
        }

        public void Login(Tuple<string, string> model)
        {
            User = model.Item1;
            Email = model.Item2;

            IsAuthenticated = true;
        }

        public void Logout()
        {
            IsAuthenticated = false;
        }
    }
}
