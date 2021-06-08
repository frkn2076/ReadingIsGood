using Microsoft.EntityFrameworkCore;
using Stock.DataAccess;
using Stock.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stock.Repository.Implementation
{
    public class StockRepository : IStockRepository
    {
        private readonly StockDBContext _context;
        public StockRepository(StockDBContext context) => _context = context;

        private async Task<StockEntity> InsertAsync(StockEntity stock) => (await _context.Stocks.AddAsync(stock)).Entity;

        private async Task<StockEntity> GetByProductId(int productId) => await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == productId);

        private async Task<List<StockEntity>> GetAll() => await _context.Stocks.AsNoTracking().ToListAsync();

        private void Remove(StockEntity stock) => _context.Stocks.Remove(stock);

        private void IncreaseQuantity(StockEntity stock, int quantity) => stock.Quantity += quantity;

        private void DecreaseQuantity(StockEntity stock, int quantity) => stock.Quantity -= quantity;

        private async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);

        #region Explicit Interface Definitions
        Task<StockEntity> IStockRepository.InsertAsync(StockEntity stock) => InsertAsync(stock);
        Task<StockEntity> IStockRepository.GetByProductId(int productId) => GetByProductId(productId);
        Task<List<StockEntity>> IStockRepository.GetAll() => GetAll();
        void IStockRepository.Remove(StockEntity stock) => Remove(stock);
        void IStockRepository.IncreaseQuantity(StockEntity stock, int quantity) => IncreaseQuantity(stock, quantity);
        void IStockRepository.DecreaseQuantity(StockEntity stock, int quantity) => DecreaseQuantity(stock, quantity);
        Task<int> IStockRepository.SaveChangesAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);
        #endregion
    }
}
