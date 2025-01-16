using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using Finance.Utilities.FormBuilder;
using Finance.Utilities.FormBuilder.Fields;
using Finance.Utilities.FormBuilder.Interface;
using Finance.ViewModels;

namespace Finance.Forms.FormBuilders
{
    public class AccountFormBuilder : FormBuilderBase, IFormBuilder
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;
        private readonly IAccountRepository _accountRepository;

        private Form _form;

        private string _accountNumber;
        public string AccountNumber
        {
            get => _accountNumber;
            set
            {
                _accountNumber = value;
                Validate(nameof(AccountNumber), _accountNumber, _submitRelayCommand);
            }
        }

        private string _owner;
        public string Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                Validate(nameof(Owner), _owner, _submitRelayCommand);
            }
        }

        private RelayCommand _cancelRelayCommand;
        private RelayCommand _submitRelayCommand;

        public AccountFormBuilder(
            IAuthenticationService authenticationService, 
            INavigationService navigationService, 
            IAccountRepository accountRepository)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            _accountRepository = accountRepository;

            _form = new Form();

            BuildTitle();
            BuildForm();
            BuildSubmitButton();
            BuildCancelButton();
        }

        public void BuildCancelButton()
        {
            _cancelRelayCommand = new RelayCommand(obj => { _navigationService.NavigateTo<AccountsViewModel>(); }, obj => true);

            _form.SetCancelButton("Cancel");
            _form.SetCancelCommand(_cancelRelayCommand);
        }

        public void BuildForm()
        {
            GridField grid = new GridField(this, 2);

            grid.Children.Add(new TextBlockField("Account number", 0));
            grid.Children.Add(new TextBoxField("AccountNumber", 0));

            grid.Children.Add(new TextBlockField("Owner", 1));
            grid.Children.Add(new TextBoxField("Owner", 1));

            _form.SetGrid(grid);
        }

        public void BuildSubmitButton()
        {
            _submitRelayCommand = new RelayCommand( async obj =>
            {
                AccountModel accountModel = new AccountModel()
                {
                    AccountNumber = AccountNumber,
                    Owner = Owner,
                    UserId = _authenticationService.UserId,
                };

                await _accountRepository.AddAsync(accountModel);

                _navigationService.NavigateTo<AccountsViewModel>();
            }, obj =>
            {
                if (!string.IsNullOrWhiteSpace(AccountNumber) &&
                !string.IsNullOrWhiteSpace(Owner))
                    return true;

                return false;
            });

            _form.SetSubmitButton("Submit");
            _form.SetSubmitCommand(_submitRelayCommand);
        }

        public void BuildTitle()
        {
            _form.SetTitle("Add Account");
        }

        public Form GetForm()
        {
            return _form;
        }
    }
}
