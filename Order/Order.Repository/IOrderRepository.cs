using Order.DataAccess.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Repository
{
    public interface IOrderRepository
    {
        Task<OrderEntity> Get(int id);
        Task<OrderEntity> InsertAsync(OrderEntity order);
        void Remove(OrderEntity order);
        void AddProduct(OrderEntity order, int productId);
        void RemoveProduct(OrderEntity order, int productId);
        Task<OrderEntity> CreateOrder(int accountId);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
