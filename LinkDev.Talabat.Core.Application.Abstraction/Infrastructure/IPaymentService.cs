using LinkDev.Talabat.Core.Application.Abstraction.Basket.Model;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Infrastructure
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto?> CreateOrUpdatePaymentIntent(string BasketId);
    }
}
