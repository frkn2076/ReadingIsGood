using Product.Business.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Business
{
    public interface IBusinessManager
    {
        Task<List<ProductResponseDTO>> GetAllProducts();
        Task<List<ProductResponseDTO>> GetProductsInterval(ProductIntervalRequestDTO model);
        Task<ProductResponseDTO> GetProduct(int id);
        Task<int> AddProduct(ProductRequestDTO model);
        Task<int> AddProducts(IReadOnlyList<ProductRequestDTO> model);
    }
}
