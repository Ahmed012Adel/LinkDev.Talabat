using LinkDev.Talabat.Core.Application.Abstraction.AuthService;
using LinkDev.Talabat.Core.Application.Abstraction.AuthService.Model;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Core.Application.Services.Auth
{
    internal class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
    {
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null) throw new BadRequestException("Invalide login");

            var result = await signInManager.CheckPasswordSignInAsync(user , model.Password,lockoutOnFailure : true);
           
            if (!result.Succeeded) throw new BadRequestException("Invalide login");

            var response = new UserDto()
            {
                Id = user.Id,
                Name = user.DisplayName,
                Email = user.Email!,
                Token = "todo"
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

            if(result.Succeeded) throw new ValidationException() { Errors = result.Errors.Select(E=>E.Description) };

            var response = new UserDto()
            {
                Id = user.Id,
                Name = user.DisplayName,
                Email = user.Email!,
                Token = "todo"
            };
            return response;
        }
    }
}
