using LinkDev.Talabat.Core.Domain.Contracts;

namespace LinkDev.Talabat.APIs.Extention
{
    public static class IntializerExtention
    {
        public static async Task<WebApplication> IntializerStoreContextAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateAsyncScope();
            var service = scope.ServiceProvider;
            var storeContextIntializer = service.GetRequiredService<IStoreContextIntializer>();
            var storeIDentityContextIntializer = service.GetRequiredService<IStoreIdentityDbIntializer>();


            var LoggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                await storeContextIntializer.UpdateDatabaseAsync();
                await storeContextIntializer.DataSeedAsync();

                await storeIDentityContextIntializer.UpdateDatabaseAsync();
                await storeIDentityContextIntializer.DataSeedAsync();

            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(string.Empty, "An Error during Migrate");
            }
            return app;
        }

    }
}
