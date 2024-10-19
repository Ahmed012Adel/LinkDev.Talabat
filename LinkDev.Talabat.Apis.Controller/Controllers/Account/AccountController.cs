using LinkDev.Talabat.Apis.Controller.Controllers.BaseController;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.AuthService.Model;
using Microsoft.AspNetCore.Mvc;

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
    }
}
