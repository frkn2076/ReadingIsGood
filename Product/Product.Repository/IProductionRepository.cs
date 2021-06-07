using Product.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Repository
{
    public interface IProductionRepository
    {
        Task<Production> GetProductAsync(int id);
        Task<List<Production>> GetProductsAsync();
        Task<List<Production>> GetProductsIntervalAsync(int startIndex, int count);
        Task<Production> InsertAsync(Production production);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
