using LinkDev.Talabat.Apis.Controller.Controllers.BaseController;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Order.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkDev.Talabat.Apis.Controller.Controllers.Order
{
    public class OrderController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(CreateOrderDto order)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Order = await serviceManager.OrdersService.CreateOrderAsync(buyerEmail!, order);

            return Ok(Order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrdersService.GetOrderForUserAsync(buyerEmail!);

            return Ok(result);
        }

        [HttpGet("{id}")] // base/Api/Order/id
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrder(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrdersService.GetOrderByIdAsync(buyerEmail!, id);
            
            return Ok(result);
        }

        [HttpGet("deliveryMethods")]// base/Api/Order/deliveryMethods

        public async Task<ActionResult<IEnumerable<DeliveryMethodeDto>>> GetDeliveryMethod()
        {
            var result = await serviceManager.OrdersService.GetDeliveryMethodAsync();
            return Ok(result);
        }

    }
}
