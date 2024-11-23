using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Order.Models;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LinkDev.Talabat.Core.Application.Mapping
{
    internal class OrderPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemsDto, string>
    {

        public string Resolve(OrderItem source, OrderItemsDto destination, string destMember, ResolutionContext context)
        {
            if (!source.Product.PictureUrl.IsNullOrEmpty())
                return $"{configuration["URLs:BaseUrl"]}/{source.Product.PictureUrl}";

            return string.Empty;
        }
    }
}
