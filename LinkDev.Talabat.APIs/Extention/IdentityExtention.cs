using LinkDev.Talabat.Core.Application.Abstraction.AuthService;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructrure.Persistence._Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
namespace LinkDev.Talabat.APIs.Extention
{
    public static class IdentityExtention
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services , IConfiguration configuration)
        {

            services.Configure<JwtSettings>(configuration.GetSection("JWTSetting"));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(Func<IAuthService>), serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<IAuthService>();
            });

            return services;
        }
    }
}
