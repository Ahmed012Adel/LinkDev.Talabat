using LinkDev.Talabat.Core.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.Seeds
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContxt dbcontext)
        {
            if (!dbcontext.Brands.Any())
            {
                var brandFile = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructrure.Persistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandFile);

                if (brands?.Count > 0) 
                {
                    await dbcontext.Set<ProductBrand>().AddRangeAsync(brands);  
                    await dbcontext.SaveChangesAsync();
                }

            }
            
            if (!dbcontext.categories.Any())
            {
                var categoriesFile = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructrure.Persistence/Data/Seeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesFile);

                if (categories?.Count > 0) 
                {
                    await dbcontext.Set<ProductCategory>().AddRangeAsync(categories);  
                    await dbcontext.SaveChangesAsync();
                }

            }

            if (!dbcontext.Products.Any())
            {
                var ProductsFile = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructrure.Persistence/Data/Seeds/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsFile);

                if (Products?.Count > 0) 
                {
                    await dbcontext.Set<Product>().AddRangeAsync(Products);  
                    await dbcontext.SaveChangesAsync();
                }

            }


        }
    }
}
