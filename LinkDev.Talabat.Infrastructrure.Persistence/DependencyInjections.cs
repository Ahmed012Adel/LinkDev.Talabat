using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructrure.Persistence.Data;
using LinkDev.Talabat.Infrastructrure.Persistence.Data.Interceptor;
using LinkDev.Talabat.Infrastructrure.Persistence.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructrure.Persistence
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<StoreDbContxt>(optionBuilder =>
            {
                optionBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("StoreContext"));
            });

            service.AddScoped(typeof(IUniteOfWork), typeof(UnitOfWork_Store_));
            service.AddScoped(typeof(IStoreContextIntializer) , typeof(StoreContextntializer));
            //service.AddScoped(typeof(ISaveChangesInterceptor), typeof(BaseAuditableEntityInterceptor));
            return service;
        }
    }
}