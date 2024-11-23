using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Infrastructrure.Persistence.Data.Config.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.Config.OrderConfig
{
    internal class DeliveryMethodConfugirations : BaseEntityConfiguration<DeliveryMethod , int>
    {
        public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            base.Configure(builder);
            builder.Property(Method => Method.Cost)
                .HasColumnType("decimal(8,2)");
        }
    }
}
