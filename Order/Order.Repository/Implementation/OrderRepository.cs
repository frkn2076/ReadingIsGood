using Order.DataAccess;
using Order.DataAccess.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDBContext _context;

        public OrderRepository(OrderDBContext context) => _context = context;

        private async Task<OrderEntity> Get(int id) => await _context.Orders.FindAsync(id);

        private async Task<OrderEntity> CreateOrder(int accountId) => (await _context.Orders.AddAsync(new OrderEntity() { AccountId = accountId })).Entity;

        private async Task<OrderEntity> InsertAsync(OrderEntity order) => (await _context.Orders.AddAsync(order)).Entity;

        private void Remove(OrderEntity order) => _context.Orders.Remove(order);

        private void AddProduct(OrderEntity order, int productId) => order.Products.Add(productId);

        private void RemoveProduct(OrderEntity order, int productId) => order.Products.Remove(productId);

        private async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);

        #region Explicit Interface Definitions
        Task<OrderEntity> IOrderRepository.Get(int id) => Get(id);
        Task<OrderEntity> IOrderRepository.InsertAsync(OrderEntity order) => InsertAsync(order);
        void IOrderRepository.Remove(OrderEntity order) => Remove(order);
        void IOrderRepository.AddProduct(OrderEntity order, int productId) => AddProduct(order, productId);
        void IOrderRepository.RemoveProduct(OrderEntity order, int productId) => RemoveProduct(order, productId);
        Task<OrderEntity> IOrderRepository.CreateOrder(int accountId) => CreateOrder(accountId);
        Task<int> IOrderRepository.SaveChangesAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);
        #endregion
    }
}
