﻿using LinkDev.Talabat.Infrastructrure.Persistence.Data;
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
                optionBuilder.UseSqlServer(configuration.GetConnectionString("StoreContext"));
            });

            return service;
        }
    }
}