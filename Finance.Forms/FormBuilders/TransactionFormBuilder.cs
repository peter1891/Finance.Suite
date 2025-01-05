using Finance.Core;
using Finance.Repository.Interface.Models;
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

        public DateTime Date { get; set; } = DateTime.Today;
        public double Amount { get; set; }
        public string Type { get; set; }
        public string CounterParty { get; set; }
        public string Description { get; set; }

        public TransactionFormBuilder(INavigationService navigationService, ITransactionRepository transactionRepository)
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
            Grid grid = new GridField(this, 3);

            grid.Children.Add(new TextBlockField("Transaction date", 0));
            grid.Children.Add(new DatePickerField("Date", 0));

            grid.Children.Add(new TextBlockField("Amount", 1));
            grid.Children.Add(new TextBoxField("Amount", 1));

            grid.Children.Add(new TextBlockField("Counter Party", 2));
            grid.Children.Add(new TextBoxField("CounterParty", 2));

            _form.SetGrid(grid);
        }

        public void BuildSubmitButton()
        {
            RelayCommand submitRelayCommand = new RelayCommand(obj =>
            {
                // Data ophalen uit het form

                // Data opslaan in de SQLite DB

                _navigationService.NavigateTo<TransactionsViewModel>();
            }, obj => true);

            _form.SetSubmitButton("Submit");
            _form.SetSubmitCommand(submitRelayCommand);
        }

        public void BuildTitle()
        {
            _form.SetTitle("Add Transaction");
        }

        public Form GetForm()
        {
            return _form;
        }
    }
}
