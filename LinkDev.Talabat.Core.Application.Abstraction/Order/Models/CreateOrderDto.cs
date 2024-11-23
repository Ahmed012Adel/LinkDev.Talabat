using LinkDev.Talabat.Core.Application.Abstraction.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Order.Models
{
    public class CreateOrderDto
    {
        public required string BasketId { get; set; }
        public int DeliveryMethod { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
