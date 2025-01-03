using Finance.Core;
using Finance.Services.Navigation.Interface;
using Finance.Utilities.FormBuilder;
using Finance.Utilities.FormBuilder.Fields;
using Finance.Utilities.FormBuilder.Interface;
using Finance.ViewModels;

namespace Finance.Forms
{
    public class AccountFormBuilder : IFormBuilder
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
            GridField grid = new GridField(this, 2);

            grid.Children.Add(new TextBlockField("AccountNumber", 0));
            grid.Children.Add(new TextBoxField("AccountNumber", 0));

            grid.Children.Add(new TextBlockField("Owner", 1));
            grid.Children.Add(new TextBoxField("Owner", 1));

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
