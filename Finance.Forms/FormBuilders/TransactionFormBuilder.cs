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
            INavigationService navigationService, 
            ITransactionRepository transactionRepository)
        {
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
            Grid grid = new GridField(this, 8);

            grid.Children.Add(new LabelField("Account", 0));
            grid.Children.Add(new TextBlockField("TransactionModel.Account.AccountNumber", 0));

            grid.Children.Add(new LabelField("Transaction date", 1));
            grid.Children.Add(new TextBlockField("TransactionModel.Date", 1));

            grid.Children.Add(new LabelField("Amount", 2));
            grid.Children.Add(new TextBlockField("TransactionModel.Amount", 2));

            grid.Children.Add(new LabelField("Name", 3));
            grid.Children.Add(new TextBlockField("TransactionModel.Name", 3));

            grid.Children.Add(new LabelField("Counter Party", 4));
            grid.Children.Add(new TextBlockField("TransactionModel.CounterParty", 4));

            grid.Children.Add(new LabelField("Description", 5));
            grid.Children.Add(new TextBlockField("TransactionModel.Description", 5));

            grid.Children.Add(new LabelField("Recurring", 6));
            grid.Children.Add(new CheckBoxField("Recurring", 6));

            grid.Children.Add(new LabelField("Comment", 7));
            grid.Children.Add(new TextBoxField("Comment", 7));

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
            _form.SetTitle("Transaction");
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
