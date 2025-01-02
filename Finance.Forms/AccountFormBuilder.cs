using Finance.Core;
using Finance.Services.Navigation.Interface;
using Finance.Utilities.FormBuilder;
using Finance.Utilities.FormBuilder.Base;
using Finance.Utilities.FormBuilder.Interface;
using Finance.ViewModels;
using System.Windows.Controls;

namespace Finance.Forms
{
    public class AccountFormBuilder : FormBuilderBase, IFormBuilder
    {
        private readonly INavigationService _navigationService;

        private Form _form;

        public string AccountNumber { get; set; }
        public string Owner { get; set; }

        public AccountFormBuilder(INavigationService navigationService)
        {
            _navigationService = navigationService;

            _form = new Form();

            BuildTitle();
            BuildForm();
            BuildSubmitButton();
            BuildCancelButton();
        }

        public void BuildCancelButton()
        {
            RelayCommand cancelRelayCommand = new RelayCommand(obj => { _navigationService.NavigateTo<AccountsViewModel>(); }, obj => true);

            _form.SetCancelButton("Cancel");
            _form.SetCancelCommand(cancelRelayCommand);
        }

        public void BuildForm()
        {
            Grid grid = BuildGrid(this, 2);

            TextBlock accountNumberBlock = BuildTextBlockField("AccountNumber", 0);
            grid.Children.Add(accountNumberBlock);

            TextBox accountNumberBox = BuildTextBoxField("AccountNumber", 0);
            grid.Children.Add(accountNumberBox);

            TextBlock ownerBlock = BuildTextBlockField("Owner", 1);
            grid.Children.Add(ownerBlock);

            TextBox ownerBox = BuildTextBoxField("Owner", 1);
            grid.Children.Add(ownerBox);

            _form.SetGrid(grid);
        }

        public void BuildSubmitButton()
        {
            RelayCommand submitRelayCommand = new RelayCommand(obj => { _navigationService.NavigateTo<AccountsViewModel>(); }, obj => true);

            _form.SetSubmitButton("Submit");
            _form.SetSubmitCommand(submitRelayCommand);
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
