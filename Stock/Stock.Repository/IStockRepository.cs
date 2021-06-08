using Stock.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public interface IStockRepository
    {
        Task<StockEntity> InsertAsync(StockEntity stock);
        Task<StockEntity> GetByProductId(int productId);
        Task<List<StockEntity>> GetAll();
        void Remove(StockEntity stock);
        void IncreaseQuantity(StockEntity stock, int quantity);
        void DecreaseQuantity(StockEntity stock, int quantity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
