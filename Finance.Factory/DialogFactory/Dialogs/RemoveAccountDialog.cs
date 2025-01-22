using Finance.Core;
using Finance.Repository.Interface.Models;
using Finance.Services.Navigation.Interface;

namespace Finance.Factory.DialogFactory.Dialogs
{
    public class RemoveAccountDialog<T> : Dialog where T : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private readonly IAccountRepository _accountRepository;

        public RemoveAccountDialog(
            INavigationService navigationService,
            IAccountRepository accountRepository
            )
        {
            _navigationService = navigationService;

            _accountRepository = accountRepository;
        }

        public override void ExecuteNo(object obj)
        {
            _navigationService.NavigateTo<T>();
        }

        public override void ExecuteYes(object obj)
        {
            if(obj != null)
                _accountRepository.DeleteAsync(int.Parse(obj.ToString()));

            _navigationService.NavigateTo<T>();
        }
    }
}
