using Finance.Core;
using Finance.Enums;
using Finance.Factory.TileFactory.Interface;
using Finance.Factory.TileFactory.Tiles;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Factory.TileFactory
{
    public class TileFactory : ITileFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private ViewModelBase _tile;

        public TileFactory(
            IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
        }

        public ViewModelBase GetTile()
        {
            return _tile;
        }

        public void SetTile(TileType tileType)
        {
            switch(tileType)
            {
                case TileType.Allocations:
                    _tile = _serviceProvider.GetRequiredService<AllocationsTile>();
                    break;
                case TileType.IncomeExpenses:
                    _tile = _serviceProvider.GetRequiredService<IncomeExpensesTile>();
                    break;
                case TileType.TotalBalance:
                    _tile = _serviceProvider.GetRequiredService<TotalBalanceTile>();
                    break;
                case TileType.Transactions:
                    _tile = _serviceProvider.GetRequiredService<TransactionsTile>();
                    break;
                default:
                    break;
            }
        }
    }
}
