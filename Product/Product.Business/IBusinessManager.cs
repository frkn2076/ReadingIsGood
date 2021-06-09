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
        Task AddProduct(ProductRequestDTO model);
        Task AddProducts(IReadOnlyList<ProductRequestDTO> model);
    }
}
