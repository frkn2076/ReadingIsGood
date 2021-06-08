using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {

        public GatewayController()
        {
        }

        [HttpGet("selam")]
        public string Get()
        {
            return string.Empty;
        }
    }
}
