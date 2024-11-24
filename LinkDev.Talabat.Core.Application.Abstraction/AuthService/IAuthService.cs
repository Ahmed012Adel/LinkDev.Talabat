using LinkDev.Talabat.Core.Application.Abstraction.AuthService.Model;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.AuthService
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);

        Task<UserDto> RigisterAsync(RigisterDto model);
        Task<UserDto> GetCurrentUser(ClaimsPrincipal claims);
        Task<AddressDto> GetUSerAddress(ClaimsPrincipal claims);
    }
}
