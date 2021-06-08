using Order.Business.DTOs;
using Order.DataAccess.Entities;
using System.Threading.Tasks;

namespace Order.Business
{
    public interface IBusinessManager
    {
        Task<OrderEntity> CreateOrderAsync(int accountId);
        Task<int> DeleteOrderAsync(int id);
        Task<int> AddProductToOrder(ProductOrderRequestDTO order);
        Task<int> RemoveProductFromOrder(ProductOrderRequestDTO order);
    }
}
