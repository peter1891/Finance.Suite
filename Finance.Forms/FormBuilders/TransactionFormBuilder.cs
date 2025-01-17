using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using Finance.Utilities.FormBuilder;
using Finance.Utilities.FormBuilder.Fields;
using Finance.Utilities.FormBuilder.Interface;
using Finance.ViewModels;
using System.Windows.Controls;

namespace Finance.Forms.FormBuilders
{
    public class TransactionFormBuilder : FormBuilderBase, IFormBuilder
    {
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
        private readonly ITransactionRepository _transactionRepository;

        private Form _form;

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private double _amount;
        public double Amount 
        { 
            get => _amount; 
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private string _counterParty;
        public string CounterParty
        {
            get => _counterParty;
            set
            {
                _counterParty = value;
                OnPropertyChanged(nameof(CounterParty));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public AccountModel AccountModel { get; set; }

        public TransactionFormBuilder(
            IAuthenticationService authenticationService, 
            INavigationService navigationService, 
            ITransactionRepository transactionRepository)
        {
            AuthenticationService = authenticationService;
            _navigationService = navigationService;
            _transactionRepository = transactionRepository;

            _form = new Form();

            this.Date = DateTime.Now;

            BuildTitle();
            BuildForm();
            BuildSubmitButton();
            BuildCancelButton();
        }

        public void BuildCancelButton()
        {
            RelayCommand cancelRelayCommand = new RelayCommand(obj => { _navigationService.NavigateTo<TransactionsViewModel>(); }, obj => true);

            _form.SetCancelButton("Cancel");
            _form.SetCancelCommand(cancelRelayCommand);
        }

        public void BuildForm()
        {
            Grid grid = new GridField(this, 4);

            grid.Children.Add(new TextBlockField("Account", 0));
            grid.Children.Add(new ComboBoxField("AuthenticationService.UserModel.Accounts", "AccountNumber", "AccountModel", 0));

            grid.Children.Add(new TextBlockField("Transaction date", 1));
            grid.Children.Add(new DatePickerField("Date", 1));

            grid.Children.Add(new TextBlockField("Amount", 2));
            grid.Children.Add(new TextBoxField("Amount", 2));

            grid.Children.Add(new TextBlockField("Counter Party", 3));
            grid.Children.Add(new TextBoxField("CounterParty", 3));

            _form.SetGrid(grid);
        }

        public void BuildSubmitButton()
        {
            RelayCommand submitRelayCommand = new RelayCommand(async obj =>
            {
                TransactionModel transactionModel = new TransactionModel()
                {
                    Date = this.Date,
                    Amount = this.Amount,
                    Type = this.Type,
                    Account = this.AccountModel,
                    CounterParty = this.CounterParty,
                    Description = this.Description,
                };

                await _transactionRepository.AddAsync(transactionModel);

                _navigationService.NavigateTo<TransactionsViewModel>();
            }, obj => true);

            _form.SetSubmitButton("Submit");
            _form.SetSubmitCommand(submitRelayCommand);
        }

        public void BuildTitle()
        {
            _form.SetTitle("Add Transaction");
        }

        public async Task<Form> GetFormAsync(int editId = 0)
        {
            return _form;
        }
    }
}
