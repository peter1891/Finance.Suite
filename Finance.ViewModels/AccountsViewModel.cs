using Finance.Core;
using Finance.Models;
using Finance.Services.Navigation.Interface;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class AccountsViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public ObservableCollection<AccountModel> AccountModels { get; set; } = new ObservableCollection<AccountModel>();

        public ICommand NavigateAddCommand { get; set; }

        public AccountsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateAddCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("account"); }, obj => true);
        }
    }
}
