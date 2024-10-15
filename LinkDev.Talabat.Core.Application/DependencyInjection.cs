using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using LinkDev.Talabat.Core.Application.Services.Basket;
using LinkDev.Talabat.Core.Domain.Contracts.Infrustructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection service) 
        {

            //service.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            service.AddAutoMapper(typeof(MappingProfile)/*.Assembly || M => M.AddProfile<MappingProfile>*/);


            service.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            //service.AddScoped(typeof(IBasketService) , typeof(BasketService));
            //service.AddScoped(typeof(Func<IBasketService>) , typeof(Func<BasketService>) );

            service.AddScoped(typeof(Func<IBasketService>), (serviceProvider) =>
            {
                var mapper = serviceProvider.GetRequiredService<IMapper>();
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var basketRepo = serviceProvider.GetRequiredService<IBasketRepostry>();

                return new BasketService(basketRepo, mapper, configuration);
            });

            return service;
        }
    }
}
