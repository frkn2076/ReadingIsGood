using Order.Business.DTOs;
using Order.DataAccess.Entities;
using Order.Repository;
using System.Threading.Tasks;

namespace Order.Business.Implementation
{
    public class BusinessManager : IBusinessManager
    {
        private readonly IOrderRepository _orderRepository;
        public BusinessManager(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        private async Task<OrderEntity> CreateOrderAsync(int accountId)
        {
            var orderEntity = await _orderRepository.CreateOrder(accountId);

            await _orderRepository.SaveChangesAsync();

            return orderEntity;
        }

        private async Task<int> DeleteOrderAsync(int id)
        {
            var orderEntity = await _orderRepository.Get(id);

            _orderRepository.Remove(orderEntity);

            var response = await _orderRepository.SaveChangesAsync();

            return response;

        }

        private async Task<int> AddProductToOrder(ProductOrderRequestDTO order)
        {
            var orderEntity = await _orderRepository.Get(order.OrderId);

            _orderRepository.AddProduct(orderEntity, order.ProductId);

            var response = await _orderRepository.SaveChangesAsync();

            return response;
        }

        private async Task<int> RemoveProductFromOrder(ProductOrderRequestDTO order)
        {
            var orderEntity = await _orderRepository.Get(order.OrderId);

            _orderRepository.RemoveProduct(orderEntity, order.ProductId);

            var response = await _orderRepository.SaveChangesAsync();

            return response;
        }

        #region Explicit Interface Definitions
        Task<OrderEntity> IBusinessManager.CreateOrderAsync(int accountId) => CreateOrderAsync(accountId);
        Task<int> IBusinessManager.DeleteOrderAsync(int id) => DeleteOrderAsync(id);
        Task<int> IBusinessManager.AddProductToOrder(ProductOrderRequestDTO order) => AddProductToOrder(order);
        Task<int> IBusinessManager.RemoveProductFromOrder(ProductOrderRequestDTO order) => RemoveProductFromOrder(order);
        #endregion

    }
}
