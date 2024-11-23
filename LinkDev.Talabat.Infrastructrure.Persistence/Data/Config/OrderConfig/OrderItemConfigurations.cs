using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Infrastructrure.Persistence.Data.Config.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.Config.OrderConfig
{
    internal class OrderItemConfigurations : BaseAuditableEntityConfigurations<OrderItem , int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(I => I.Product, product => product.WithOwner());

            builder.Property(I => I.Price)
                .HasColumnType("decimal(8,2)");
        }
    }
}
