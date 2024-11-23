using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
    public enum OrderStatus
    {
        Pending = 1,
        PaymentRecived = 2,
        paymentFailde = 3
    }
}
