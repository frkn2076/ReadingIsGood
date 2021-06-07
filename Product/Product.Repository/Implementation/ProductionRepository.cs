using Microsoft.EntityFrameworkCore;
using Product.DataAccess;
using Product.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Repository.Implementation
{
    public class ProductionRepository : IProductionRepository
    {
        private readonly ProductDBContext _context;

        public ProductionRepository(ProductDBContext context) => _context = context;

        private async Task<Production> GetProductAsync(int id) => await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        private async Task<List<Production>> GetProductsAsync() => await _context.Products.AsNoTracking().ToListAsync();
        private async Task<List<Production>> GetProductsIntervalAsync(int startIndex, int count)
            => await _context.Products.Skip(startIndex).Take(count).ToListAsync();
        private async Task<Production> InsertAsync(Production production) => (await _context.Products.AddAsync(production)).Entity;
        private async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);

        #region Explicit Interface Definitions
        Task<Production> IProductionRepository.GetProductAsync(int id) => GetProductAsync(id);
        Task<List<Production>> IProductionRepository.GetProductsAsync() => GetProductsAsync();
        Task<List<Production>> IProductionRepository.GetProductsIntervalAsync(int startIndex, int count) => GetProductsIntervalAsync(startIndex, count);
        Task<Production> IProductionRepository.InsertAsync(Production production) => InsertAsync(production);
        Task<int> IProductionRepository.SaveChangesAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);
        #endregion
    }
}
