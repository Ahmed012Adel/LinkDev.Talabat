using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructrure.Persistence._Identity;
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
            #region Store DbContext

            service.AddDbContext<StoreDbContxt>(optionBuilder =>
            {
                optionBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("StoreContext"));
            });

            service.AddScoped(typeof(IStoreContextIntializer), typeof(StoreDbContextntializer));
            service.AddScoped(typeof(ISaveChangesInterceptor), typeof(AuditInterceptor));
            #endregion

            service.AddDbContext<StoreIdentityDbContext>((seviceProvider , options) =>
            {
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("IdentityContext"))
                    .AddInterceptors(seviceProvider.GetRequiredService<AuditInterceptor>());
            });

            service.AddScoped(typeof(AuditInterceptor));
            service.AddScoped<IStoreIdentityDbIntializer, StoreIdentityDbIntializer>();

            service.AddScoped(typeof(IUniteOfWork), typeof(UnitOfWork_Store_));

            return service;
        }
    }
}