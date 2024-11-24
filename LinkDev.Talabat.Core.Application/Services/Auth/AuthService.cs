using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.AuthService;
using LinkDev.Talabat.Core.Application.Abstraction.AuthService.Model;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Models;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Application.Extentions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkDev.Talabat.Core.Application.Services.Auth
{
    public class AuthService(
        IMapper mapper,
        IOptions<JwtSettings> jwtSetting,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager) : IAuthService
    {

        private readonly JwtSettings _jwtSettings = jwtSetting.Value;

        public async Task<UserDto> GetCurrentUser(ClaimsPrincipal claims)
        {
            var email = claims.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email!);

            return new UserDto()
            {
                Id = user!.Id,
                Email = user.Email!,
                Name = user.DisplayName,
                Token = await GenerateTokenAsync(user)
            };
        }

        public async Task<AddressDto> GetUSerAddress(ClaimsPrincipal claims)
        {
            var user = await userManager.FindUserWithAddress(claims);
            var address = mapper.Map<AddressDto>(user!.Address);

            return address;
        }

        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null) throw new UnAuthorizedException("Invalide login");

            var result = await signInManager.CheckPasswordSignInAsync(user , model.Password,lockoutOnFailure : true);
           
            if (!result.Succeeded) throw new UnAuthorizedException("Invalide login");

            var response = new UserDto()
            {
                Id = user.Id,
                Name = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user),
            };
            return response;
        }

        public async Task<UserDto> RigisterAsync(RigisterDto model)
        {
            var user = new ApplicationUser()
            {
                DisplayName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.Phone
            };

            var result = await userManager.CreateAsync(user,model.Password);

            //if(result.Succeeded) throw new ValidationException() { Errors = result.Errors.Select(E=>E.Description) };

            var response = new UserDto()
            {
                Id = user.Id,
                Name = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user)
            };
            return response;
        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var authClaims = new[]
            {
                new Claim(ClaimTypes.PrimarySid , user.Id),
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.GivenName , user.DisplayName)
            }.Union(await userManager.GetClaimsAsync(user)).ToList();

            foreach (var role in await userManager.GetRolesAsync(user))
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));


            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));


            var TokenObj = new JwtSecurityToken(

                audience: _jwtSettings.Audience,
                issuer: _jwtSettings.Issuer,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinatues),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)


                );

            return new JwtSecurityTokenHandler().WriteToken(TokenObj);
        }
    }
}
