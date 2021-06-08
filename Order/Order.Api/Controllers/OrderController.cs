using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Api.ViewModels.Requests;
using Order.Business;
using Order.Business.DTOs;
using Shared.Constants;
using Shared.Extensions;
using Shared.Messages;

namespace Order.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IBusinessManager _businessManager;

        public OrderController(ILogger<OrderController> logger, IBusinessManager businessManager)
        {
            _logger = logger;
            _businessManager = businessManager;
        }

        /// <summary>
        /// Adds the given product to order. 
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProductToOrder(ProductOrderRequestViewModel request)
        {
            var userId = HttpContext.User.GetClaim(CommonConstants.AccountIdClaimName);
            var model = request.Adapt<ProductOrderRequestDTO>();
            model = TypeAdapter.Adapt(new { AccountId = userId }, model);
            await _businessManager.AddProductToOrder(model);

            return Ok(BaseResponse.Success);
        }

    }
}
