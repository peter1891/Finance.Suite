using Finance.Core;
using Finance.Enums;
using Finance.Factory.TileFactory.Interface;
using Finance.Services.Navigation.Interface;

namespace Finance.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;
        private readonly ITileFactory _tileFactory;

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

        private object _incomeExpensesView;
        public object IncomeExpensesView
        {
            get => _incomeExpensesView;
            set
            {
                _incomeExpensesView = value;
                OnPropertyChanged(nameof(IncomeExpensesView));
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
            IServiceProvider serviceProvider,
            ITileFactory tileFactory
            )
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
            _tileFactory = tileFactory;

            _tileFactory.SetTile(TileType.Allocations);
            AllocationsView = _tileFactory.GetTile();

            _tileFactory.SetTile(TileType.IncomeExpenses);
            IncomeExpensesView = _tileFactory.GetTile();

            _tileFactory.SetTile(TileType.TotalBalance);
            TotalBalanceView = _tileFactory.GetTile();

            _tileFactory.SetTile(TileType.Transactions);
            TransactionsView = _tileFactory.GetTile();
        }
    }
}
