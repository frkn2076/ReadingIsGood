using Mapster;
using Product.Business.DTOs;
using Product.DataAccess.Entities;
using Product.Infra.Exceptions;
using Product.Repository;
using Shared.Error;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Business.Implementation
{
    public class BusinessManager : IBusinessManager
    {
        private readonly IProductionRepository _productionRepository;

        public BusinessManager(IProductionRepository productionRepository) => _productionRepository = productionRepository;

        private async Task<List<ProductResponseDTO>> GetAllProducts()
        {
            var productEntities = await _productionRepository.GetProductsAsync();

            var response = productEntities.Adapt<List<ProductResponseDTO>>();

            return response;
        }

        private async Task<List<ProductResponseDTO>> GetProductsInterval(ProductIntervalRequestDTO model)
        {
            var productEntities = await _productionRepository.GetProductsIntervalAsync(model.StartIndex, model.Count);
            if(productEntities is null || productEntities.Count == 0)
                throw new ProductNotFoundException();

            var response = productEntities.Adapt<List<ProductResponseDTO>>();

            return response;
        }

        private async Task<ProductResponseDTO> GetProduct(int id)
        {
            var product = await _productionRepository.GetProductAsync(id);
            if (product is null)
                throw new ProductNotFoundException();

            var response = product.Adapt<ProductResponseDTO>();

            return response;
        }

        private async Task<int> AddProduct(ProductRequestDTO model)
        {
            var productionEntity = model.Adapt<Production>();

            var product = await _productionRepository.InsertAsync(productionEntity);
            if (product is null)
                throw new SomethingWentWrongDuringDatabaseOperationException();

            var response = await _productionRepository.SaveChangesAsync();

            return response;
        }

        private async Task<int> AddProducts(IReadOnlyList<ProductRequestDTO> model)
        {
            var productions = model.Adapt<List<Production>>();

            productions.ForEach(async production => await _productionRepository.InsertAsync(production));

            var response = await _productionRepository.SaveChangesAsync();

            return response;
        }

        #region Explicit Interface Definitions
        Task<List<ProductResponseDTO>> IBusinessManager.GetAllProducts() => GetAllProducts();
        Task<List<ProductResponseDTO>> IBusinessManager.GetProductsInterval(ProductIntervalRequestDTO model) => GetProductsInterval(model);
        Task<ProductResponseDTO> IBusinessManager.GetProduct(int id) => GetProduct(id);
        Task<int> IBusinessManager.AddProduct(ProductRequestDTO model) => AddProduct(model);
        Task<int> IBusinessManager.AddProducts(IReadOnlyList<ProductRequestDTO> model) => AddProducts(model);
        #endregion
    }
}
