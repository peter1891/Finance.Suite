using Finance.Core;
using Finance.Models;
using Finance.Services.Navigation.Interface;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class TransactionsViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public ObservableCollection<TransactionModel> TransactionModels { get; set; } = new ObservableCollection<TransactionModel>();

        public ICommand NavigateAddCommand { get; set; }

        public TransactionsViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;

            NavigateAddCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("transaction"); }, obj => true);
        }
    }
}
