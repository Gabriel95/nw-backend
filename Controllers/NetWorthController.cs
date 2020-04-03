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

        [Authorize]
        [HttpGet("currentnetworth")]
        public async Task<IActionResult> GetCurrentNetWorthCalculation()
        {
            try
            {
                var authenticateInfo = await HttpContext.GetTokenAsync("access_token");
                var userId = _authService.GetUserIdFromToken(authenticateInfo);
                if (userId.Equals(Guid.Empty))
                    return Unauthorized();
                var netWorthModel = _netWorthService.GetCurrentNetWorth(userId);
                return Ok(netWorthModel);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [Authorize]
        [HttpGet("getallnetworths")]
        public async Task<IActionResult> GetAllNetWorths()
        {
            try
            {
                var authenticateInfo = await HttpContext.GetTokenAsync("access_token");
                var userId = _authService.GetUserIdFromToken(authenticateInfo);
                if (userId.Equals(Guid.Empty))
                    return Unauthorized();

                var netWorths = _netWorthService.GetAllNetWorths(userId);
                return Ok(netWorths);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNetWorth(Guid id)
        {
            try
            {
                var authenticateInfo = await HttpContext.GetTokenAsync("access_token");
                var userId = _authService.GetUserIdFromToken(authenticateInfo);
                if (userId.Equals(Guid.Empty))
                    return Unauthorized();

                var netWorth = _netWorthService.DeleteNetWorth(userId, id);
                return Ok(netWorth);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}