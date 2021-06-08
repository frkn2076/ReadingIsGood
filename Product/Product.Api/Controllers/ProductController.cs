using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Api.ViewModels.Requests;
using Product.Api.ViewModels.Responses;
using Product.Business;
using Product.Business.DTOs;
using Shared.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IBusinessManager _businessManager;

        public ProductController(ILogger<ProductController> logger, IBusinessManager businessManager)
        {
            _logger = logger;
            _businessManager = businessManager;
        }

        /// <summary>
        /// Adds the given product. 
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProduct(ProductRequestViewModel request)
        {
            var model = request.Adapt<ProductRequestDTO>();
            await _businessManager.AddProduct(model);

            return Ok(BaseResponse.Success);
        }

        /// <summary>
        /// Adds the given products. 
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProducts(ProductsRequestViewModel request)
        {
            var model = request.Adapt<List<ProductRequestDTO>>();
            await _businessManager.AddProducts(model);

            return Ok(BaseResponse.Success);
        }

        /// <summary>
        /// Gets the products for the given number (Count), by starting the given index (StartIndex).
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductsInterval(ProductIntervalRequestViewModel request)
        {
            var model = request.Adapt<ProductIntervalRequestDTO>();
            var productModels = await _businessManager.GetProductsInterval(model);

            var products = productModels.Adapt<ProductsResponseViewModel>();
            var response = TypeAdapter.Adapt(BaseResponse.Success, products);

            return Ok(response);
        }

        /// <summary>
        /// Gets all the products. 
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            var productModels = await _businessManager.GetAllProducts();

            var products = productModels.Adapt<List<ProductResponseViewModel>>();

            var response = TypeAdapter.Adapt(BaseResponse.Success, products);

            return Ok(response);
        }

        /// <summary>
        /// Gets the product by given id. 
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _businessManager.GetProduct(id);

            var product = model.Adapt<ProductResponseViewModel>();

            var response = TypeAdapter.Adapt(BaseResponse.Success, product);

            return Ok(response);
        }


    }
}
