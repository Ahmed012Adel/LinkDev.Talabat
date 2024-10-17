using LinkDev.Talabat.Core.Domain.Entities.Identity;

namespace LinkDev.Talabat.Infrastructrure.Persistence._Identity.Config
{
    internal class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.Property(A=>A.UserId).ValueGeneratedOnAdd();
        }
    }
}
