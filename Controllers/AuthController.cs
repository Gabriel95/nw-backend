using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using nw_api.Data.Entities;
using nw_api.Interfaces;
using nw_api.Models;

namespace nw_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
    
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody]UserLoginModel userLogin)
        {
            try
            {
                var user = _userService.GetUserByEmailAndPassword(userLogin);
                if (user == null)
                    return NotFound("Email or Password Incorrect");
                var token = _authService.GenerateToken(user);
                return Ok(token);
            }
            catch (Exception e)
            {
                return Problem(statusCode: 500, detail: e.Message);
            }
        }
        
        [AllowAnonymous]
        [HttpPost("signup")]
        public ActionResult Post([FromBody] UserRegisterModel userRegisterModel)
        {
            try
            {
                if (!userRegisterModel.Password.Equals(userRegisterModel.ConfirmPassword))
                    return BadRequest("Passwords don't match");
                
                var user = _userService.Insert(userRegisterModel);
                var token = _authService.GenerateToken(user);
                return Ok(token);
            }
            catch (PostgresException pe)
            {
                return pe.SqlState.Equals("23505")
                    ? BadRequest("Email already in use!")
                    : Problem("Problem with Database", statusCode: 500);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}