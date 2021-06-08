using Stock.Business.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stock.Business
{
    public interface IBusinessManager
    {
        Task IncreaseStock(StockRequestDTO model);
        Task DecreaseStock(StockRequestDTO model);
        Task<StockResponseDTO> GetStockDetails(int productId);
        Task<List<StockResponseDTO>> GetAllStocks();
    }
}
