﻿using LinkDev.Talabat.Core.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts.Infrustructure
{
    public interface IBasketGeneric
    {
        Task<CustomerBasket?> GetAsync(string id);

        Task<CustomerBasket?> UpdateBasket(CustomerBasket basket,TimeSpan timeToLife);

        Task DeleteBasket(string id);
    }
}