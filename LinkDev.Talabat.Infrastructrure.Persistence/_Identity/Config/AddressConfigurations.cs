using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructrure.Persistence.Common;

namespace LinkDev.Talabat.Infrastructrure.Persistence._Identity.Config
{
    [DbContextTypeAttribute(typeof(StoreIdentityDbContext))]
    internal class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.Property(A=>A.UserId).ValueGeneratedOnAdd();
        }
    }
}
