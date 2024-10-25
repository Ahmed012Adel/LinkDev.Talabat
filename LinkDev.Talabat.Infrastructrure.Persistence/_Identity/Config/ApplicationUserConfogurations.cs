using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructrure.Persistence.Common;

namespace LinkDev.Talabat.Infrastructrure.Persistence._Identity.Config
{
    [DbContextTypeAttributr(typeof(StoreIdentityDbContext))]
    internal class ApplicationUserConfogurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(U => U.UserName)
                   .HasColumnType("varchar")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasOne(u => u.Address)
                   .WithOne(A => A.User)
                   .HasForeignKey<Address>(A => A.UserId);
        }
    }
}
