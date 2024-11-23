using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Order.Models
{
    public class OrderItemsDto
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string PictureUrl { get; set; }

        public decimal Price { get; set; }
        public int Quanity { get; set; }
    }
}
