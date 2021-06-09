using Microsoft.EntityFrameworkCore;
using Product.DataAccess;
using Product.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDBContext _context;

        public ProductRepository(ProductDBContext context) => _context = context;

        private async Task<ProductEntity> GetProductAsync(int id) => await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        private async Task<List<ProductEntity>> GetProductsAsync() => await _context.Products.AsNoTracking().ToListAsync();

        private async Task<List<ProductEntity>> GetProductsIntervalAsync(int startIndex, int count)
            => await _context.Products.Skip(startIndex).Take(count).ToListAsync();

        private async Task<ProductEntity> InsertAsync(ProductEntity product) => (await _context.Products.AddAsync(product)).Entity;

        private async Task<bool> IsExistAsync(ProductEntity product) => await _context.Products
            .AnyAsync(x => x.Name == product.Name && x.Volume == product.Volume && x.Weight == product.Weight);

        private async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);

        #region Explicit Interface Definitions
        Task<ProductEntity> IProductRepository.GetProductAsync(int id) => GetProductAsync(id);
        Task<List<ProductEntity>> IProductRepository.GetProductsAsync() => GetProductsAsync();
        Task<List<ProductEntity>> IProductRepository.GetProductsIntervalAsync(int startIndex, int count) => GetProductsIntervalAsync(startIndex, count);
        Task<ProductEntity> IProductRepository.InsertAsync(ProductEntity product) => InsertAsync(product);
        Task<bool> IProductRepository.IsExistAsync(ProductEntity product) => IsExistAsync(product);
        Task<int> IProductRepository.SaveChangesAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);
        #endregion
    }
}
