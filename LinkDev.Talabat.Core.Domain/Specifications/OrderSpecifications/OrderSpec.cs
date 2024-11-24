using LinkDev.Talabat.Core.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.OrderSpecifications
{
    public class OrderSpec : BaseSpecificatins<Order, int>
    {
        public OrderSpec(string buyerEmail, int orderId) : base(order => order.Id == orderId && order.BuyerEmail == buyerEmail)
        {
            AddIncludes();
        }
        public OrderSpec(string buyerEmail) : base(order => order.BuyerEmail == buyerEmail)
        {
            AddIncludes();
            AddOrderByDesc(order => order.OrderTime);

        }

        private protected override void AddIncludes()
        {
            base.AddIncludes();

            Includes.Add(order => order.OrderItems);
            Includes.Add(order => order.deliveryMethod!);
        }
    }
}