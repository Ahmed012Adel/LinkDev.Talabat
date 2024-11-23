using LinkDev.Talabat.Core.Domain.Common;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.Config.Base
{
   // [DbContextType(typeof(StoreDbContxt))]
    internal class BaseEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(E => E.Id)
                .ValueGeneratedOnAdd();

         
        }
    }
}
