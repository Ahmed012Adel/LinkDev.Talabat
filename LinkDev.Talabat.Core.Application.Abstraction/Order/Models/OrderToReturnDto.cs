using LinkDev.Talabat.Core.Application.Abstraction.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Order.Models
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }

        public required string BuyerEmail { get; set; }
        public DateTime OrderTime { get; set; }
        public required string Status { get; set; } 
        public required AddressDto ShippingAddress { get; set; }


        public int? deliveryMethodId { get; set; }
        public  string? deliveryMethod { get; set; }

        public required ICollection<OrderItemsDto> OrderItems { get; set; } 

        public decimal SupTotal { get; set; }
        public decimal Total { get /*{ return SupTotal + deliveryMethod!.Cost; }*/; set; }

        // Getter Method
        //public decimal GetTotal => SupTotal + deliveryMethod!.Cost;

    }
}
