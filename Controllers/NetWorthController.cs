using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nw_api.Interfaces;
using nw_api.Models;

namespace nw_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NetWorthController : Controller
    {
        private readonly INetWorthService _netWorthService;
        private readonly IAuthService _authService;

        public NetWorthController(INetWorthService netWorthService, IAuthService authService)
        {
            _netWorthService = netWorthService;
            _authService = authService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> NetWorth([FromBody] NetWorthModel netWorthModel)
        {
            try
            {
                var authenticateInfo = await HttpContext.GetTokenAsync("access_token");
                var userId = _authService.GetUserIdFromToken(authenticateInfo);
                if (userId.Equals(Guid.Empty))
                    return Unauthorized();
                netWorthModel.UserId = userId;
                _netWorthService.AddNetWorth(netWorthModel);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}