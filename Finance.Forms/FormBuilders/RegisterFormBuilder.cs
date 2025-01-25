using Finance.Core;
using Finance.CustomControls;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using Finance.Utilities.Encoder.Interface;
using Finance.Utilities.FormBuilder;
using Finance.Utilities.FormBuilder.Fields;
using Finance.Utilities.FormBuilder.Interface;
using Finance.ViewModels;
using System.Security;

namespace Finance.Forms.FormBuilders
{
    public class RegisterFormBuilder : FormBuilderBase, IFormBuilder
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;
        private readonly IUserRepository _userRepository;

        private readonly IPasswordEncoder _passwordEncoder;

        private Form _form;

        private string _firstName;
        public string FirstName 
        { 
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));

                Validate(nameof(FirstName), value, _submitRelayCommand);
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));

                Validate(nameof(LastName), value, _submitRelayCommand);
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));

                Validate(nameof(Email), value, _submitRelayCommand);
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

                Validate(nameof(Password), value, _submitRelayCommand);
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

        private RelayCommand _cancelRelayCommand;
        private RelayCommand _submitRelayCommand;

        public RegisterFormBuilder(
            IAuthenticationService authenticationService, 
            INavigationService navigationService, 
            IUserRepository userRepository,
            IPasswordEncoder passwordEncoder)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            _userRepository = userRepository;

            _passwordEncoder = passwordEncoder;

            _form = new Form();

            BuildTitle();
            BuildForm();
            BuildSubmitButton();
            BuildCancelButton();
        }

        public void BuildCancelButton()
        {
            _cancelRelayCommand = new RelayCommand(obj => _navigationService.NavigateTo<LoginViewModel>(), obj => true);

            _form.SetCancelButton("Cancel");
            _form.SetCancelCommand(_cancelRelayCommand);
        }

        public void BuildForm()
        {
            GridField grid = new GridField(this, 5);

            grid.Children.Add(new LabelField("First name", 0));
            grid.Children.Add(new TextBoxField("FirstName", 0));

            grid.Children.Add(new LabelField("Last name", 1));
            grid.Children.Add(new TextBoxField("LastName", 1));

            grid.Children.Add(new LabelField("E-mail", 2));
            grid.Children.Add(new TextBoxField("Email", 2));

            grid.Children.Add(new LabelField("Password", 3));
            grid.Children.Add(new PasswordBoxField("Password", 3).PasswordBox);

            grid.Children.Add(new ErrorMessageField("ErrorMessage", 4));

            _form.SetGrid(grid);
        }

        public void BuildSubmitButton()
        {
            _submitRelayCommand = new RelayCommand( async obj =>
            {
                if (!await _userRepository.ValidateNewUser(this.Email))
                {
                    string password = _passwordEncoder.GetHashPassword(Password);

                    UserModel userModel = new UserModel()
                    {
                        FirstName = this.FirstName,
                        LastName = this.LastName,
                        Email = this.Email,
                        Password = password
                    };
                    await _userRepository.AddAsync(userModel);

                    _navigationService.NavigateTo<LoginViewModel>();
                }
                else
                {
                    ErrorMessage = "E-mail is already registered.";
                }
             }, obj => 
             {
                 if (!string.IsNullOrWhiteSpace(FirstName) &&
                 !string.IsNullOrWhiteSpace(LastName) &&
                 !string.IsNullOrWhiteSpace(Email) &&
                 Password != null)
                 {
                     if (Password.Length > 0)
                         return true;
                 }

                 return false;
             });

            _form.SetSubmitButton("Register");
            _form.SetSubmitCommand(_submitRelayCommand);
        }

        public void BuildTitle()
        {
            _form.SetTitle("Create Account");
        }

        public async Task<Form> GetFormAsync(int editId = 0)
        {
            return _form;
        }
    }
}
