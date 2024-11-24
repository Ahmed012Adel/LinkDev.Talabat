using LinkDev.Talabat.Apis.Controller.Controllers.BaseController;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Order.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Apis.Controller.Controllers.Order
{
    public class OrderController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpGet] 
        public async Task<ActionResult<OrderToReturnDto>> CreateOrderAsync(CreateOrderDto order)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Order = await serviceManager.OrdersService.CreateOrderAsync(buyerEmail!, order);

            return Ok(Order);
        }


    }
}
