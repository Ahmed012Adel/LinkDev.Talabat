using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data
{
    public class StoreContextntializer(StoreDbContxt dbContxt) : IStoreContextIntializer
    {
        

        public async Task UpdateDatabaseAsync()
        {
            var pendingMigration = await dbContxt.Database.GetPendingMigrationsAsync();

            if (pendingMigration.Any())
            {
                await dbContxt.Database.MigrateAsync();
            }

        }

        public async Task DataSeedAsync()
        {
            if (!dbContxt.Brands.Any())
            {
                var brandFile = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructrure.Persistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandFile);

                if (brands?.Count > 0)
                {
                    await dbContxt.Set<ProductBrand>().AddRangeAsync(brands);
                    await dbContxt.SaveChangesAsync();
                }

            }

            if (!dbContxt.categories.Any())
            {
                var categoriesFile = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructrure.Persistence/Data/Seeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesFile);

                if (categories?.Count > 0)
                {
                    await dbContxt.Set<ProductCategory>().AddRangeAsync(categories);
                    await dbContxt.SaveChangesAsync();
                }

            }

            if (!dbContxt.Products.Any())
            {
                var ProductsFile = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructrure.Persistence/Data/Seeds/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsFile);

                if (Products?.Count > 0)
                {
                    await dbContxt.Set<Product>().AddRangeAsync(Products);
                    await dbContxt.SaveChangesAsync();
                }

            }

            if (!dbContxt.DeliveryMethods.Any())
            {
                var DeliveryMethod = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructrure.Persistence/Data/Seeds/delivery.json");
                var Methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethod);

                if (Methods?.Count > 0)
                {
                    await dbContxt.Set<DeliveryMethod>().AddRangeAsync(Methods);
                    await dbContxt.SaveChangesAsync();
                }

            }


        }
    }
}
