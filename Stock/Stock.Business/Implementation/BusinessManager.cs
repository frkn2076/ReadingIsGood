using Mapster;
using Stock.Business.DTOs;
using Stock.Infra.Exceptions;
using Stock.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Business.Implementation
{
    public class BusinessManager : IBusinessManager
    {
        private readonly IStockRepository _stockRepository;

        public BusinessManager(IStockRepository stockRepository) => _stockRepository = stockRepository;

        private async Task IncreaseStock(StockRequestDTO model)
        {
            var stockEntity = await _stockRepository.GetByProductId(model.Stock.ProductId);
            if (stockEntity is null)
                throw new StockNotFoundException();

            _stockRepository.IncreaseQuantity(stockEntity, model.Stock.Quantity);

            await _stockRepository.SaveChangesAsync();
        }

        private async Task DecreaseStock(StockRequestDTO model)
        {
            var stockEntity = await _stockRepository.GetByProductId(model.Stock.ProductId);
            if (stockEntity is null)
                throw new StockNotFoundException();

            _stockRepository.DecreaseQuantity(stockEntity, model.Stock.Quantity);

            await _stockRepository.SaveChangesAsync();
        }

        private async Task<StockResponseDTO> GetStockDetails(int productId)
        {
            var stockEntity = await _stockRepository.GetByProductId(productId);
            if (stockEntity is null)
                throw new StockNotFoundException();

            var response = stockEntity.Adapt<StockResponseDTO>();

            return response;
        }

        private async Task<List<StockResponseDTO>> GetAllStocks()
        {
            var stockEntities = await _stockRepository.GetAll();
            if (stockEntities is null || stockEntities.Count == 0)
                throw new StockNotFoundException();

            var response = stockEntities.Select(x => x.Adapt<StockResponseDTO>()).ToList();

            return response;
        }

        #region Explicit Interface Definitions
        Task IBusinessManager.IncreaseStock(StockRequestDTO model) => IncreaseStock(model);
        Task IBusinessManager.DecreaseStock(StockRequestDTO model) => DecreaseStock(model);
        Task<StockResponseDTO> IBusinessManager.GetStockDetails(int productId) => GetStockDetails(productId);
        Task<List<StockResponseDTO>> IBusinessManager.GetAllStocks() => GetAllStocks();
        #endregion
    }
}
