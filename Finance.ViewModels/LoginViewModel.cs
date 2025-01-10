using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using Finance.Utilities.Encoder.Interface;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;
        private readonly IUserRepository _userRepository;

        private readonly IPasswordEncoder _passwordEncoder;

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));

                Validate(nameof(Email), value, (RelayCommand)ExecuteLoginCommand);
            }
        }

        private SecureString _password;
        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));

                Validate(nameof(Password), value, (RelayCommand)ExecuteLoginCommand);
            }
        }

        private string _errorMessage;
        public string ErrorMessage 
        { 
            get => _errorMessage; 
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand ExecuteLoginCommand { get; }

        public LoginViewModel(
            IAuthenticationService authenticationService, 
            INavigationService navigationService, 
            IUserRepository userRepository, 
            IPasswordEncoder passwordEncoder)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            _userRepository = userRepository;

            _passwordEncoder = passwordEncoder;

            ExecuteLoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object obj)
        {
            if (!String.IsNullOrWhiteSpace(Email) && Password != null)
                return true;

            return false;
        }

        private async void ExecuteLogin(object obj)
        {
            string password = _passwordEncoder.GetHashPassword(Password);

            NetworkCredential networkCredential = new NetworkCredential(Email, password);

            if (await _userRepository.AuthenticateUserAsync(networkCredential))
            {
                UserModel userModel = await _userRepository.GetUserAsync(networkCredential);
                _authenticationService.Login(userModel);

                _navigationService.NavigateTo<DashboardViewModel>();
            }
            else
            {
                ErrorMessage = "Invalid e-mail or password.";
            }
        }
    }
}
