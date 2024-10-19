using LinkDev.Talabat.Apis.Controller;
using LinkDev.Talabat.Apis.Controller.Error;
using LinkDev.Talabat.APIs.Extention;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructrure.Persistence;
using LinkDev.Talabat.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs
{

    // ASP.Net Core Web APIs - Project Struceture
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            var config = webApplicationBuilder.Configuration;
            #region Configure Service

            webApplicationBuilder.Services
                .AddControllers()
                .ConfigureApiBehaviorOptions(option =>
                {
                    option.SuppressModelStateInvalidFilter = false;
                    option.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count > 0)
                                                             .Select(P => new ApiValidationErrorResponse.ValidationError()
                                                             {
                                                                 Field = P.Key,
                                                                 Errors = P.Value!.Errors.Select(P => P.ErrorMessage)
                                                             });
                        return new BadRequestObjectResult(
                            new ApiValidationErrorResponse() 
                            {
                                Errors = errors 
                            });
                    };
                })
                .AddApplicationPart(typeof(AssemblyInformationApi).Assembly);

            // Add services to the container.

            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer().AddSwaggerGen();
            webApplicationBuilder.Services.AddPersistenceService(config)
                                          .AddInfrustructureServices(config)
                                          .AddIdentityService(config);
            webApplicationBuilder.Services.AddApplicationService();

            webApplicationBuilder.Services.AddHttpContextAccessor();
            webApplicationBuilder.Services.AddScoped(typeof(ILoggedUserInService), typeof(LoggedUserInService));

            //webApplicationBuilder.Services.AddScoped(typeof(ILoggedUserInService), typeof(LoggedUserInService));


            #endregion
            try
            {

                var app = webApplicationBuilder.Build();
            #region Database Intializer

            await app.IntializerStoreContextAsync();

            #endregion

            #region Configure Kestral Middleware

            app.UseMiddleware<ExceptionHandelerMaddelware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllers(); 

            #endregion

            app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message,ex.StackTrace);
            }

        }
    }
}
