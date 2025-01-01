using Finance.Core;
using Finance.Services.Navigation.Interface;
using System.Windows.Input;

namespace Finance.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get { return _navigationService; }
            set
            {
                _navigationService = value;
                OnPropertyChanged(nameof(NavigationService));
            }
        }

        public ICommand NavigateTransactionsCommand { get; set; }

        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigationService.NavigateTo<TransactionsViewModel>();

            NavigateTransactionsCommand = new RelayCommand(obj => { _navigationService.NavigateTo<TransactionsViewModel>(); }, obj => true);
        }

        //private readonly IServiceProvider _serviceProvider;

        //private MenuView _menuView;

        //public object CurrentView { get; set; }

        //public MainViewModel(IServiceProvider serviceProvider)
        //{
        //    _serviceProvider = serviceProvider;

        //    _menuView = _serviceProvider.GetRequiredService<MenuView>();

        //    CurrentView = _menuView;
        //}
    }
}
