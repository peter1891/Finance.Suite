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

        private UserModel _userModel;
        public UserModel UserModel
        {
            get { return _userModel; }
            set
            {
                _userModel = value;
                OnPropertyChanged(nameof(UserModel));
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

        public void Login(UserModel userModel)
        {
            this.UserModel = userModel;

            ButtonText = this.UserModel.FirstName;
            IsAuthenticated = true;
        }

        public void Logout()
        {
            this.UserModel = null;

            ButtonText = "Sign in";
            IsAuthenticated = false;
        }
    }
}
