using LinkDev.Talabat.Core.Domain.Entities.Product;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data
{
    public class StoreDbContxt : DbContext
    {
        public StoreDbContxt(DbContextOptions option) : base(option) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> categories { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
    }

}
