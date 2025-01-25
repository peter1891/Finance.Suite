using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Repository.Repository.Models;
using Finance.Services.Authentication.Interface;
using Finance.Services.Navigation.Interface;
using Finance.Utilities.FormBuilder;
using Finance.Utilities.FormBuilder.Fields;
using Finance.Utilities.FormBuilder.Interface;
using Finance.ViewModels;

namespace Finance.Forms.FormBuilders
{
    public class AllocationFormBuilder : FormBuilderBase, IFormBuilder
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;
        private readonly IAccountRepository _accountRepository;
        private readonly IAllocationRepository _allocationRepository;

        private Form _form;

        public List<AccountModel> AccountModels { get; set; }

        private AccountModel _accountModel;
        public AccountModel AccountModel
        {
            get => _accountModel;
            set => _accountModel = value;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                Validate(nameof(Name), _name, _submitRelayCommand);
            }
        }

        private double _amount;
        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                Validate(nameof(Amount), _amount, _submitRelayCommand);
            }
        }

        private double _targetAmount;
        public double TargetAmount
        {
            get => _targetAmount;
            set => _targetAmount = value;
        }

        private DateTime _targetDate;
        public DateTime TargetDate
        {
            get => _targetDate;
            set => _targetDate = value;
        }

        private AllocationModel _allocationModel;

        private RelayCommand _cancelRelayCommand;
        private RelayCommand _submitRelayCommand;

        public AllocationFormBuilder(
            IAuthenticationService authenticationService, 
            INavigationService navigationService, 
            IAccountRepository accountRepository,
            IAllocationRepository allocationRepository
            )
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            _accountRepository = accountRepository;
            _allocationRepository = allocationRepository;

            _form = new Form();

            GetAccountsAsync();

            BuildTitle();
            BuildForm();
            BuildSubmitButton();
            BuildCancelButton();
        }

        private async void GetAccountsAsync()
        {
            AccountModels = new List<AccountModel>(await _accountRepository.GetAccountsByAuthenticatedIdAsync(_authenticationService.UserId));

            if (AccountModels.Count != 0)
                AccountModel = AccountModels[0];
        }

        public void BuildCancelButton()
        {
            _cancelRelayCommand = new RelayCommand(obj => { _navigationService.NavigateTo<AllocationsViewModel>(); }, obj => true);

            _form.SetCancelButton("Cancel");
            _form.SetCancelCommand(_cancelRelayCommand);
        }

        public void BuildForm()
        {
            GridField grid = new GridField(this, 4);

            grid.Children.Add(new LabelField("Account", 0));
            if (_allocationModel == null)
                grid.Children.Add(new ComboBoxField("AccountModels", "AccountNumber", "AccountModel", 0));
            else
                grid.Children.Add(new TextBlockField("AccountModel.AccountNumber", 0));

            grid.Children.Add(new LabelField("Name", 1));
            grid.Children.Add(new TextBoxField("Name", 1));

            grid.Children.Add(new LabelField("Target amount", 2));
            grid.Children.Add(new TextBoxField("TargetAmount", 2));

            grid.Children.Add(new LabelField("Target date", 3));
            grid.Children.Add(new DatePickerField("TargetDate", 3));

            _form.SetGrid(grid);
        }

        public void BuildSubmitButton()
        {
            _submitRelayCommand = new RelayCommand(async obj =>
            {
                if (_allocationModel == null)
                {
                    AllocationModel allocationModel = new AllocationModel()
                    {
                        Name = this.Name,
                        Amount = 0,
                        TargetAmount = this.TargetAmount,
                        TargetDate = this.TargetDate,
                        AccountId = _accountModel.Id,
                    };

                    await _allocationRepository.AddAsync(allocationModel);
                }
                else
                {
                    _allocationModel.TargetAmount = this.TargetAmount;
                    _allocationModel.TargetDate = this.TargetDate;

                    await _allocationRepository.UpdateAsync(_allocationModel);
                }

                _navigationService.NavigateTo<AllocationsViewModel>();
            }, obj =>
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    return true;

                return false;
            });

            _form.SetSubmitButton("Submit");
            _form.SetSubmitCommand(_submitRelayCommand);
        }

        public void BuildTitle()
        {
            _form.SetTitle("Add Allocation");
        }

        public async Task<Form> GetFormAsync(int editId = 0)
        {
            if (editId != 0)
            {
                _allocationModel = await _allocationRepository.GetByIdAsync(editId);

                this.Name = _allocationModel.Name;
                this.Amount = _allocationModel.Amount;
                this.TargetAmount = _allocationModel.TargetAmount;
                this.TargetDate = _allocationModel.TargetDate;

                BuildForm();
            }

            return _form;
        }
    }
}
