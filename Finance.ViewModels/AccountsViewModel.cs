using Finance.Core;
using Finance.Services.Navigation.Interface;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class AccountsViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public ICommand NavigateAddCommand { get; set; }

        public AccountsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateAddCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("account"); }, obj => true);
        }
    }
}
