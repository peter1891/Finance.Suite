using Finance.Core;
using Finance.Enums;

namespace Finance.Factory.TileFactory.Interface
{
    public interface ITileFactory
    {
        void SetTile(TileType tileType);
        ViewModelBase GetTile();
    }
}
