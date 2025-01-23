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

        private bool _recurring;
        public bool Recurring
        {
            get => _recurring;
            set
            {
                _recurring = value;
                OnPropertyChanged(nameof(Recurring));
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        private TransactionModel _transactionModel;
        public TransactionModel TransactionModel
        {
            get => _transactionModel;
            set
            {
                _transactionModel = value;
                OnPropertyChanged(nameof(TransactionModel));
            }
        }

        public TransactionFormBuilder(
            IAuthenticationService authenticationService, 
            INavigationService navigationService, 
            ITransactionRepository transactionRepository)
        {
            AuthenticationService = authenticationService;
            _navigationService = navigationService;
            _transactionRepository = transactionRepository;

            _form = new Form();

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
            Grid grid = new GridField(this, 6);

            grid.Children.Add(new LabelField("Account", 0));
            grid.Children.Add(new TextBlockField("TransactionModel.Account.AccountNumber", 0));

            grid.Children.Add(new LabelField("Transaction date", 1));
            grid.Children.Add(new TextBlockField("TransactionModel.Date", 1));

            grid.Children.Add(new LabelField("Amount", 2));
            grid.Children.Add(new TextBlockField("TransactionModel.Amount", 2));

            grid.Children.Add(new LabelField("Counter Party", 3));
            grid.Children.Add(new TextBlockField("TransactionModel.CounterParty", 3));

            grid.Children.Add(new LabelField("Recurring", 4));
            grid.Children.Add(new CheckBoxField("Recurring", 4));

            grid.Children.Add(new LabelField("Comment", 5));
            grid.Children.Add(new TextBoxField("Comment", 5));

            _form.SetGrid(grid);
        }

        public void BuildSubmitButton()
        {
            RelayCommand submitRelayCommand = new RelayCommand(async obj =>
            {
                if (this.TransactionModel != null)
                {
                    this.TransactionModel.Recurring = Recurring;
                    this.TransactionModel.Comment = Comment;

                    await _transactionRepository.UpdateAsync(this.TransactionModel);
                }

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
            if (editId != 0)
            {
                this.TransactionModel = await _transactionRepository.GetByIdAsync(editId);

                this.Recurring = _transactionModel.Recurring;
                this.Comment = _transactionModel.Comment;
            }

            return _form;
        }
    }
}
