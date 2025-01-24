using Finance.Core;
using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Navigation.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class TransactionsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ITransactionRepository _transactionRepository;

        public ObservableCollection<TransactionModel> TransactionModels { get; set; } = new ObservableCollection<TransactionModel>();

        public ICommand NavigateEditCommand { get; }

        public TransactionsViewModel(INavigationService navigationService, ITransactionRepository transactionRepository)
        {
            _navigationService = navigationService;
            _transactionRepository = transactionRepository;

            GetTransactionsAsync();

            NavigateEditCommand = new RelayCommand(obj => { _navigationService.NavigateTo<FormViewModel>("transaction", (int)obj); }, obj => true);
        }

        private async void GetTransactionsAsync()
        {
            if (TransactionModels.Count != 0)
                TransactionModels.Clear();

            var transactionModels = await _transactionRepository.GetAllAsync();
            foreach (TransactionModel transactionModel in transactionModels)
                TransactionModels.Add(transactionModel);
        }
    }
}
