﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Order.Models
{
    public class DeliveryMethodeDto
    {
        public int Id { get; set; }

        public required string ShortName { get; set; }
        public required string Description { get; set; }
        public decimal Cost { get; set; }
        public required string DeliveryTime { get; set; }
    }
}
