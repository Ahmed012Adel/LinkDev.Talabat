using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
    // owned to Order Entity
    //[Owned]
    public class Address
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string street { get; set; }
        public required string  City { get; set; }
        public required string  Country { get; set; }
    }
}
