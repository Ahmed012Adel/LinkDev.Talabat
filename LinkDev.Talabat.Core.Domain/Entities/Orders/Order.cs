using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
    public class Order : BaseAuditableEntity<int>
    {
        public required string BuyerEmail { get; set; }
        public DateTime OrderTime { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public required Address ShippingAddress { get; set; }
        
        
        public int? deliveryMethodId { get; set; }
        public virtual DeliveryMethod? deliveryMethod { get; set; }

        public virtual required ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        public decimal SupTotal { get; set; }
        //[NotMapped]
        //public decimal Total { get{ return SupTotal + deliveryMethod!.Cost; } }

        // Getter Method
        public decimal GetTotal => SupTotal + deliveryMethod!.Cost;

        public string PaymentIntenedId { get; set; } = "";
    }
}
