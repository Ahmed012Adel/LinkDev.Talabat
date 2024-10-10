using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
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

            return service;
        }
    }
}
