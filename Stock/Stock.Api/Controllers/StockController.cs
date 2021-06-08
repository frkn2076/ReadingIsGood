using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Messages;
using Stock.Api.ViewModels.Requests;
using Stock.Api.ViewModels.Responses;
using Stock.Business;
using Stock.Business.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IBusinessManager _businessManager;

        public StockController(ILogger<StockController> logger, IBusinessManager businessManager)
        {
            _logger = logger;
            _businessManager = businessManager;
        }

        /// <summary>
        /// Increases stock as the given amount (Quantity) for the given product (ProductId). 
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> IncreaseStock(StockRequestViewModel request)
        {
            var model = request.Adapt<StockRequestDTO>();
            await _businessManager.IncreaseStock(model);

            return Ok(BaseResponse.Success);
        }

        /// <summary>
        /// Decreases stock as the given amount (Quantity) for the given product (ProductId).
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DecreaseStock(StockRequestViewModel request)
        {
            var model = request.Adapt<StockRequestDTO>();
            await _businessManager.DecreaseStock(model);

            return Ok(BaseResponse.Success);

        }

        /// <summary>
        /// Gets all stocks.
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpGet]
        [ProducesResponseType(typeof(StocksResponseViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStocks()
        {
            var models = await _businessManager.GetAllStocks();

            var stocks = models.Select(x => x.Adapt<StockResponseViewModel>()).ToArray();

            var response = new StocksResponseViewModel() { Stocks = stocks };

            response = TypeAdapter.Adapt(BaseResponse.Success, response);

            return Ok(response);

        }

        /// <summary>
        /// Gets all stocks.
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StockResponseViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDetail(int productId)
        {
            var model = await _businessManager.GetStockDetails(productId);

            var response = model.Adapt<StockResponseViewModel>();

            return Ok(response);

        }
    }
}
