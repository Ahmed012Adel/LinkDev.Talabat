using LinkDev.Talabat.Core.Application.Abstraction.Basket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Basket
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetcustomerBasketAsync(string basketId);

        Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basketDto);

        Task DeleteBasketAsync(string BasketId);
    }
}
