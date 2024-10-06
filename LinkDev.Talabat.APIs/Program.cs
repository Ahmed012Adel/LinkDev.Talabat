
using LinkDev.Talabat.APIs.Extention;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructrure.Persistence;
using LinkDev.Talabat.Infrastructrure.Persistence.Data;
using LinkDev.Talabat.Infrastructrure.Persistence.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.APIs
{

    // ASP.Net Core Web APIs - Project Struceture
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);
            
            #region Configure Service

            // Add services to the container.

            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer().AddSwaggerGen();
            webApplicationBuilder.Services.AddPersistenceService(webApplicationBuilder.Configuration);

            

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

            app.MapControllers(); 

            #endregion

            app.Run();
        }
    }
}
