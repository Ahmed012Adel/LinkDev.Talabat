using LinkDev.Talabat.Core.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.OrderSpecifications
{
    public class OrderSpec : BaseSpecificatins<Order, int>
    {
        public OrderSpec(string buyerEmail, int orderId) 
            : base(order => order.Id == orderId && order.BuyerEmail == buyerEmail)
        {
            AddIncludes();
        }
        private OrderSpec(Expression<Func<Order , bool>> critertia) 
            : base(critertia)
        {
           

        }

        private protected override void AddIncludes()
        {
            base.AddIncludes();

            Includes.Add(order => order.OrderItems);
            Includes.Add(order => order.deliveryMethod!);
        }

        public static OrderSpec BuyerEmail(string buyerEmail)
        {
            var spec = new OrderSpec(Order => Order.BuyerEmail == buyerEmail);
            spec.AddIncludes();
            spec.AddOrderByDesc(order => order.OrderTime);
            return spec;

        }

        public static OrderSpec PaymentIntent(string paymentIntentId) 
        {

            return new OrderSpec(Order => Order.PaymentIntenedId == paymentIntentId);

        }
    }
}