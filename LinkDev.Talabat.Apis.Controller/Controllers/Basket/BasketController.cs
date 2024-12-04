using LinkDev.Talabat.Apis.Controller.Controllers.BaseController;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Basket.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Apis.Controller.Controllers.Basket
{
    public class BasketController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketAsync(string id)
        {
            var basket  = await serviceManager.BasketService.GetcustomerBasketAsync(id);
            return Ok(basket);
        }


        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basketDto)
        {
            var basket = await serviceManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            

            await serviceManager.BasketService.DeleteBasketAsync(id);
        }
    }
}
