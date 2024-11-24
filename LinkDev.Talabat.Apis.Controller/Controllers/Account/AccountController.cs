using LinkDev.Talabat.Apis.Controller.Controllers.BaseController;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.AuthService.Model;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Security.Claims;

namespace LinkDev.Talabat.Apis.Controller.Controllers.Account
{
    public class AccountController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var respons = await serviceManager.AuthService.LoginAsync(model);
            return Ok(respons);
        }

        [HttpPost("rigister")]
        public async Task<ActionResult<UserDto>> Rigister(RigisterDto model)
        {
            var respons = await serviceManager.AuthService.RigisterAsync(model);
            return Ok(respons);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await serviceManager.AuthService.GetCurrentUser(User);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var address = await serviceManager.AuthService.GetUSerAddress(User);
         
            return Ok(address);
        }
        
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address) 
        {
            var result = await serviceManager.AuthService.UpdateUserAddress(User, address);
            return Ok(result);
        }

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.AuthService.EmailExist(email!));
        }

    }
}
