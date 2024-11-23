using LinkDev.Talabat.Apis.Controller.Controllers.BaseController;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Order.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkDev.Talabat.Apis.Controller.Controllers.Orders
{
    [Authorize]
    public class OrderController(IServiceManager serviceManager) :ApiControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(CreateOrderDto Order)
        {
            var buyerEmail = User.FindFirst(ClaimTypes.Email);
            var order = await serviceManager.OrdersService.CreateOrderAsync(buyerEmail!.Value,Order);

            return Ok(order);
        }
    }
}
