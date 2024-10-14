
using LinkDev.Talabat.APIs.Extention;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructrure.Persistence;
using LinkDev.Talabat.Infrastructrure.Persistence.Data;
using LinkDev.Talabat.Infrastructrure.Persistence.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using LinkDev.Talabat.Apis.Controller;
using LinkDev.Talabat.APIs.Services;
using Microsoft.AspNetCore.Mvc;
using LinkDev.Talabat.Apis.Controller.Error;

namespace LinkDev.Talabat.APIs
{

    // ASP.Net Core Web APIs - Project Struceture
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Service

            webApplicationBuilder.Services
                .AddControllers()
                .ConfigureApiBehaviorOptions(option =>
                {
                    option.SuppressModelStateInvalidFilter = false;
                    option.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count > 0)
                                                             .SelectMany(E => E.Value!.Errors)
                                                             .Select(E => E.ErrorMessage);
                        return new BadRequestObjectResult(new ApiValidationErrorResponse() {Errors = errors });
                    };
                })
                .AddApplicationPart(typeof(AssemblyInformationApi).Assembly);

            // Add services to the container.

            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer().AddSwaggerGen();
            webApplicationBuilder.Services.AddPersistenceService(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddApplicationService();

            webApplicationBuilder.Services.AddHttpContextAccessor();
            webApplicationBuilder.Services.AddScoped(typeof(ILoggedUserInService), typeof(LoggedUserInService));

            //webApplicationBuilder.Services.AddScoped(typeof(ILoggedUserInService), typeof(LoggedUserInService));


            #endregion

            var app = webApplicationBuilder.Build();

            #region Database Intializer

            await app.IntializerStoreContextAsync(); 

            #endregion

            #region Configure Kestral Middleware

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllers(); 

            #endregion

            app.Run();
        }
    }
}
