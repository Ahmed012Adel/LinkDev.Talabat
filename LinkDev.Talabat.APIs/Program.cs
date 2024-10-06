
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
           
            #region Apply Migrations(Update Database)

            using var scope = app.Services.CreateAsyncScope();
            var service = scope.ServiceProvider;
            var dbcontext = service.GetRequiredService<StoreDbContxt>();


            var LoggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var pendingMigration = dbcontext.Database.GetPendingMigrations();

                if (pendingMigration.Any())
                {
                    await dbcontext.Database.MigrateAsync();
                }

                await StoreContextSeed.SeedAsync(dbcontext);

            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(string.Empty, "An Error during Migrate");
            }

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
