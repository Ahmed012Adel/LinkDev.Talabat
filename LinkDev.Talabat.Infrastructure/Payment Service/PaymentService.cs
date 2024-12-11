using LinkDev.Talabat.Core.Application.Abstraction.Infrastructure;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Contracts.Infrustructure;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Shared;
using Microsoft.Extensions.Options;
using Stripe;
using Product = LinkDev.Talabat.Core.Domain.Entities.Product.Product;

namespace LinkDev.Talabat.Infrastructure.Payment_Service
{
    internal class PaymentService(IBasketRepostry basketRepostry , IUniteOfWork uniteOfWork , IOptions<RedisSetting> redisSetting) : IPaymentService
    {
        private readonly RedisSetting _redisSetting = redisSetting.Value; 
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var basket = await basketRepostry.GetAsync(BasketId);

            if (basket is null) return null;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await uniteOfWork.GetRepoitery<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
                if (deliveryMethod is null) return null;
                basket.ShippingPrice = deliveryMethod.Cost;
            }

            if (basket.Items.Count > 0)
            {
                var productRepo = uniteOfWork.GetRepoitery<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if (item.price != product!.Price)
                        item.price = product.Price;
                }
            }

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

                paymentIntent = await paymentIntentService.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.price * 100 * item.Quantity) + (long)basket.ShippingPrice * 100,

                };
                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
            }


            await basketRepostry.UpdateBasket(basket , TimeSpan.FromDays(_redisSetting.TimeToLiveInDays)); 
            return basket;
        }
    }
}
