using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Infrastructrure.Persistence.Data.Config.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.Config.OrderConfig
{
    internal class Orderconfigurations : BaseAuditableEntityConfigurations<Order,int>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(Order => Order.ShippingAddress, SAddress => SAddress.WithOwner());

            builder.Property(Order => Order.Status)
                .HasConversion
                (
                    (OStauts) => OStauts.ToString(),
                    (oStatus) => (OrderStatus) Enum.Parse(typeof(OrderStatus) , oStatus)
                );

            builder.Property(o => o.SupTotal)
                .HasColumnType("decimal(8,2)");

            builder.HasOne(o => o.deliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.deliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(o=>o.OrderItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
