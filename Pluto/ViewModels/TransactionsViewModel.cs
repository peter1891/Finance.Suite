using Finance.Core;
using Finance.Services.Navigation.Interface;
using Finance.Utilities.FormBuilder.Forms;
using System.Windows.Input;

namespace Pluto.ViewModels
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
