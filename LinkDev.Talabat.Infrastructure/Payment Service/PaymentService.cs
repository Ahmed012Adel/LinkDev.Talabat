using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Basket.Model;
using LinkDev.Talabat.Core.Application.Abstraction.Infrastructure;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Contracts.Infrustructure;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Shared;
using Microsoft.Extensions.Options;
using Stripe;
using Product = LinkDev.Talabat.Core.Domain.Entities.Product.Product;

namespace LinkDev.Talabat.Infrastructure.Payment_Service
{
    internal class PaymentService(IBasketRepostry basketRepostry
        ,IMapper mapper
        , IUniteOfWork uniteOfWork ,
        IOptions<RedisSetting> redisSetting,
        IOptions<StripeSetting> stripeSetting) : IPaymentService
    {
        private readonly RedisSetting _redisSetting = redisSetting.Value; 
        private readonly StripeSetting _stripeSetting = stripeSetting.Value; 

        public async Task<CustomerBasketDto?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _stripeSetting.SecretKey;
            var basket = await basketRepostry.GetAsync(BasketId);

            if (basket is null) return null;
           
            #region Check Delivery price || Cost

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await uniteOfWork.GetRepoitery<DeliveryMethod, int>().GetAsync((int)basket.DeliveryMethodId);
                if (deliveryMethod is null) return null;
                basket.ShippingPrice = deliveryMethod.Cost;
            }

            #endregion
            

            #region Check price of item == price or Product ||!

            if (basket.Items.Count > 0)
            {
                var productRepo = uniteOfWork.GetRepoitery<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if (product is null) return null;
                    if (item.price != product!.Price)
                        item.price = product.Price;
                }
            }

            #endregion


            PaymentIntent? paymentIntent = null;

            PaymentIntentService? paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId)) // craete New Payment Intent
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.price * 100 * item.Quantity) + (long)basket.ShippingPrice * 100,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);  //Integration with Stripe Service

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Update an Existing Payment Inteted
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.price * 100 * item.Quantity) + (long)basket.ShippingPrice * 100,

                };
                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options); //Integration with Stripe Service
            }


            await basketRepostry.UpdateBasket(basket , TimeSpan.FromDays(_redisSetting.TimeToLiveInDays)); 
            return mapper.Map<CustomerBasketDto>(basket);
        }
    }
}
