using Finance.Core;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using Finance.Strategy.TransactionStrategy.Transactions;
using Finance.Strategy.TransactionStrategy;
using Finance.Utilities.FormBuilder;
using Finance.Utilities.FormBuilder.Interface;
using Finance.ViewModels;
using Finance.Models;
using Microsoft.Extensions.DependencyInjection;
using Finance.Utilities.FormBuilder.Fields;
using System.Windows.Controls;
using Finance.Repository.Interface.Models;
using Finance.Enums;
using Finance.Strategy.TransactionStrategy.Transactions.Interface;

namespace Finance.Forms.FormBuilders
{
    public class ImportTransactionFormBuilder : FormBuilderBase, IFormBuilder
    {
        private readonly IServiceProvider _serviceProvider;

        private IAuthenticationService _authenticationService;
        public IAuthenticationService AuthenticationService
        {
            get => _authenticationService;
            set
            {
                _authenticationService = value;
                OnPropertyChanged(nameof(AuthenticationService));
            }
        }

        private readonly INavigationService _navigationService;
        private readonly IAccountRepository _accountRepository;

        private readonly TransactionContext _transactionContext;

        private Form _form;

        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                Validate(nameof(Path), _path, _submitRelayCommand);
            }
        }

        private AccountModel _accountModel;
        public AccountModel AccountModel
        {
            get => _accountModel;
            set
            {
                _accountModel = value;
                OnPropertyChanged(nameof(AccountModel));
            }
        }

        private RelayCommand _cancelRelayCommand;
        private RelayCommand _submitRelayCommand;

        public ImportTransactionFormBuilder(
            IServiceProvider serviceProvider,
            IAuthenticationService authenticationService,
            INavigationService navigationService,
            IAccountRepository accountRepository,
            TransactionContext transactionContext)
        {
            _serviceProvider = serviceProvider;

            AuthenticationService = authenticationService;
            _navigationService = navigationService;
            _accountRepository = accountRepository;

            _transactionContext = transactionContext;

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
            Grid grid = new GridField(this, 2);

            grid.Children.Add(new LabelField("Bank", 0));
            grid.Children.Add(new TextBlockField("AccountModel.Bank", 0));

            grid.Children.Add(new LabelField("Path", 1));
            grid.Children.Add(new FileSelectField("Path", 1));

            _form.SetGrid(grid);
        }

        public void BuildSubmitButton()
        {
            _submitRelayCommand = new RelayCommand(async obj =>
            {
                switch(AccountModel.Bank)
                {
                    case Bank.ING:
                        _transactionContext.SetTransactionContext(_serviceProvider.GetRequiredService<IngTransaction>());
                        break;
                    case Bank.Rabobank:
                        _transactionContext.SetTransactionContext(_serviceProvider.GetRequiredService<RabobankTransaction>());
                        break;
                    default:
                        break;
                }

                _transactionContext.ProcessTransaction(this.AccountModel.Id, Path);

                _navigationService.NavigateTo<AccountsViewModel>();
            }, obj => 
            {
                if (!string.IsNullOrWhiteSpace(Path))
                    return true;

                return false;
            });

            _form.SetSubmitButton("Submit");
            _form.SetSubmitCommand(_submitRelayCommand);
        }

        public void BuildTitle()
        {
            _form.SetTitle("Import Transactions");
        }

        public async Task<Form> GetFormAsync(int editId = 0)
        {
            if (editId != 0)
                this.AccountModel = await _accountRepository.GetByIdAsync(editId);

            return _form;
        }
    }
}
