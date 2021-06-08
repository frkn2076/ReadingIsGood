using Product.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Repository
{
    public interface IProductRepository
    {
        Task<ProductEntity> GetProductAsync(int id);
        Task<List<ProductEntity>> GetProductsAsync();
        Task<List<ProductEntity>> GetProductsIntervalAsync(int startIndex, int count);
        Task<ProductEntity> InsertAsync(ProductEntity product);
        Task<bool> IsExistAsync(ProductEntity product);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
