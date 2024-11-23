using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using System.Reflection;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data
{
    public class StoreDbContxt : DbContext
    {
        public StoreDbContxt(DbContextOptions option) : base(option) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly/*,
                Type => Type.GetCustomAttributes<DbContextTypeAttribute>().DbContextType == typeof(StoreDbContxt)*/);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> categories { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> Items { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    }

}
