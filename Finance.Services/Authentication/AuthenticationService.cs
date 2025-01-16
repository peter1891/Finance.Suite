using Finance.Core;
using Finance.Models;
using Finance.Services.Authentication.Interface;

namespace Finance.Services.Authentication
{
    public class AuthenticationService : ObservableObject, IAuthenticationService
    {
        private string _buttonText;
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
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

        public AuthenticationService()
        {
            ButtonText = "Sign in";
        }

        public void Login(int userId, string firstName)
        {
            UserId = userId;

            ButtonText = firstName;
            IsAuthenticated = true;
        }

        public void Logout()
        {
            UserId = 0;

            ButtonText = "Sign in";
            IsAuthenticated = false;
        }
    }
}
