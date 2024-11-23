using LinkDev.Talabat.Core.Application.Abstraction.Order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Order
{
    public interface IOrdersService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string BuyerEmail, CreateOrderDto order);
        Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail,int orderId);
        Task<IEnumerable<OrderToReturnDto>> getOrderForUserAsync(string buyerEmail);
        Task<IEnumerable<DeliveryMethodeDto>> getDeliveryMethodAsync();
    }
}
