using LinkDev.Talabat.Apis.Controller.Controllers.BaseController;
using LinkDev.Talabat.Core.Application.Abstraction.Basket.Model;
using LinkDev.Talabat.Core.Application.Abstraction.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Apis.Controller.Controllers.Payment
{
    [Authorize]
    public class PaymentController(IPaymentService paymentService) : ApiControllerBase
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
                var Result = await paymentService.CreateOrUpdatePaymentIntent(basketId);
                if (Result is null) return NotFound(Result);

            return Ok(Result);
        }
    }
}
