using LinkDev.Talabat.Core.Domain.Contracts.Infrustructure;
using LinkDev.Talabat.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace LinkDev.Talabat.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrustructureServices(this IServiceCollection services , IConfiguration configuration)
        {

            services.AddSingleton(typeof(IConnectionMultiplexer), (ServiceProvider) =>
            {
                var connectionString = configuration.GetConnectionString("Redis");
                var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString!);
                return connectionMultiplexer;
            });

            services.AddScoped(typeof(IBasketRepostry) , typeof(BasketRepostry));

            services.Configure<RedisSetting>(_ => configuration.GetSection("RedisSetting"));

            return services;
        } 
    }
}
