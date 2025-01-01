using Finance.Core;
using Finance.Services.Navigation.Interface;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class TransactionsViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public ICommand NavigateAddCommand { get; set; }

        public TransactionsViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;

            NavigateAddCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("transaction"); }, obj => true);
        }
    }
}
