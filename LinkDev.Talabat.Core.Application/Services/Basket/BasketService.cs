using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Basket.Model;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Infrustructure;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services.Basket
{
    internal class BasketService(IBasketRepostry basketrepostry , IMapper mapper , IConfiguration configuration) : IBasketService
    {
        public async Task<CustomerBasketDto> GetcustomerBasketAsync(string basketId)
        {
            var basket = await basketrepostry.GetAsync(basketId);

            if (basket is null)
                throw new NotFoundException(nameof(CustomerBasketDto),basketId);

            return mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basketDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketDto);


            var timeToLive = TimeSpan.FromDays(double.Parse(configuration.GetSection("RedisSetting")["TimeToLiveInDays"]!));

            var updatedBasket = await basketrepostry.UpdateBasket(basket, timeToLive);

            //if (updatedBasket is null)
            //    throw new BadRequestObjectResult(updatedBasket);


            return basketDto; 
        }


        public async Task DeleteBasketAsync(string BasketId)
        {
            await basketrepostry.DeleteAsync(BasketId);

            //if (!deleted) throw new BadRequestObjectResult("not deleted");

        }

    }
}
