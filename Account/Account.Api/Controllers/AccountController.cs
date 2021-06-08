using Account.Api.ViewModels.Requests;
using Account.Business;
using Account.Business.DTOs;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.Messages;
using System.Threading.Tasks;

namespace Account.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IBusinessManager _businessManager;
        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(IBusinessManager businessManager, IAuthenticationManager authenticationManager)
        {
            _businessManager = businessManager;
            _authenticationManager = authenticationManager;
        }

        /// <summary>
        /// User logins the system. 
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(RegisterRequestViewModel register)
        {
            var model = register.Adapt<AccountRequestDTO>();
            var user = await _businessManager.LoginAsync(model);

            var token = await _authenticationManager.GenerateToken((CommonConstants.AccountIdClaimName, user?.UserId?.ToString()));

            var response = TypeAdapter.Adapt(token, BaseResponse.Success);

            HttpContext.Session.SetString(CommonConstants.CustomerNameSessionKey, user?.CustomerName);

            return Ok(response);
        }

        /// <summary>
        /// User registers the system. 
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterRequestViewModel register)
        {
            var model = register.Adapt<AccountRequestDTO>();
            var user = await _businessManager.RegisterAsync(model);

            var token = await _authenticationManager.GenerateToken((CommonConstants.AccountIdClaimName, user?.UserId?.ToString()));

            var response = TypeAdapter.Adapt(token, BaseResponse.Success);

            HttpContext.Session.SetString(CommonConstants.CustomerNameSessionKey, user?.CustomerName);

            return Ok(response);
        }

        /// <summary>
        /// When access token expired, client will call that service and that service will produce new access token 
        /// with refresh token sent by header. Refresh token and its expiration date will remain same.
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = HttpContext.Request.Headers["RefreshToken"];
            var token = await _authenticationManager.RefreshToken(refreshToken);

            var response = TypeAdapter.Adapt(token, BaseResponse.Success);

            return Ok(response);
        }
    }
}
