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
        private readonly IProductRepository _productRepository;

        public BusinessManager(IProductRepository productRepository) => _productRepository = productRepository;

        private async Task<List<ProductResponseDTO>> GetAllProducts()
        {
            var productEntities = await _productRepository.GetProductsAsync();

            var response = productEntities.Adapt<List<ProductResponseDTO>>();

            return response;
        }

        private async Task<List<ProductResponseDTO>> GetProductsInterval(ProductIntervalRequestDTO model)
        {
            var productEntities = await _productRepository.GetProductsIntervalAsync(model.StartIndex, model.Count);
            if (productEntities is null || productEntities.Count == 0)
                throw new ProductNotFoundException();

            var response = productEntities.Adapt<List<ProductResponseDTO>>();

            return response;
        }

        private async Task<ProductResponseDTO> GetProduct(int id)
        {
            var product = await _productRepository.GetProductAsync(id);
            if (product is null)
                throw new ProductNotFoundException();

            var response = product.Adapt<ProductResponseDTO>();

            return response;
        }

        private async Task AddProduct(ProductRequestDTO model)
        {
            var productEntity = model.Adapt<ProductEntity>();

            var isExist = await _productRepository.IsExistAsync(productEntity);
            if (isExist)
                throw new ProductAlreadyExistsException();

            var product = await _productRepository.InsertAsync(productEntity);
            if (product is null)
                throw new SomethingWentWrongDuringDatabaseOperationException();

            await _productRepository.SaveChangesAsync();
        }

        private async Task AddProducts(IReadOnlyList<ProductRequestDTO> model)
        {
            var products = model.Adapt<List<ProductEntity>>();

            foreach (var product in products)
            {
                var isExist = await _productRepository.IsExistAsync(product);
                if (isExist)
                    throw new ProductAlreadyExistsException();

                await _productRepository.InsertAsync(product);
            }

            await _productRepository.SaveChangesAsync();
        }

        #region Explicit Interface Definitions
        Task<List<ProductResponseDTO>> IBusinessManager.GetAllProducts() => GetAllProducts();
        Task<List<ProductResponseDTO>> IBusinessManager.GetProductsInterval(ProductIntervalRequestDTO model) => GetProductsInterval(model);
        Task<ProductResponseDTO> IBusinessManager.GetProduct(int id) => GetProduct(id);
        Task IBusinessManager.AddProduct(ProductRequestDTO model) => AddProduct(model);
        Task IBusinessManager.AddProducts(IReadOnlyList<ProductRequestDTO> model) => AddProducts(model);
        #endregion
    }
}
