using Finance.Core;
using Finance.Enums;
using Finance.Services.Navigation.Interface;
using Finance.ViewModels.Tiles;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;

        private object _allocationsView;
        public object AllocationsView
        {
            get => _allocationsView;
            set
            {
                _allocationsView = value;
                OnPropertyChanged(nameof(AllocationsView));
            }
        }

        private object _expensesView;
        public object ExpensesView
        {
            get => _expensesView;
            set
            {
                _expensesView = value;
                OnPropertyChanged(nameof(ExpensesView));
            }
        }

        private object _incomeView;
        public object IncomeView
        {
            get => _incomeView;
            set
            {
                _incomeView = value;
                OnPropertyChanged(nameof(IncomeView));
            }
        }

        private object _totalBalanceView;
        public object TotalBalanceView
        {
            get => _totalBalanceView;
            set
            {
                _totalBalanceView = value;
                OnPropertyChanged(nameof(TotalBalanceView));
            }
        }

        private object _transactionsView;
        public object TransactionsView
        {
            get => _transactionsView;
            set
            {
                _transactionsView = value;
                OnPropertyChanged(nameof(TransactionsView));
            }
        }

        public DashboardViewModel(
            INavigationService navigationService,
            IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;

            AllocationsView = _serviceProvider.GetRequiredService<AllocationsTileViewModel>();

            ExpensesView = _serviceProvider.GetRequiredService<ExpensesTileViewModel>();

            IncomeView = _serviceProvider.GetRequiredService<IncomeTileViewModel>();

            TotalBalanceView = _serviceProvider.GetRequiredService<TotalBalanceTileViewModel>();

            TransactionsView = _serviceProvider.GetRequiredService<TransactionsTileViewModel>();
        }
    }
}
