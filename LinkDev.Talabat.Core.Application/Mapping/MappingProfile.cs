using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Basket.Model;
using LinkDev.Talabat.Core.Application.Abstraction.Common.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Employee.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Order.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Product.Models;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace LinkDev.Talabat.Core.Application.Mapping
{
    internal  class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.Brand,o=>o.MapFrom(s=>s.Brand!.Name))
                .ForMember(d=>d.Category,o=>o.MapFrom(s=>s.Category!.Name))
                //.ForMember(d=>d.PictureUrl , o=>o.MapFrom(s => $"{"https://localhost:7029"}/{s.PictureUrl}"))
                .ForMember(d=>d.PictureUrl , o => o.MapFrom<ProudctUrlResolver>());

            CreateMap<Employee, EmployeeToReturnDto>();

            CreateMap<CustomerBasket , CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();


            CreateMap<Order, OrderToReturnDto>()
                           .ForMember(Dest => Dest.deliveryMethod, option => option.MapFrom(src => src.deliveryMethod!.ShortName));

            CreateMap<OrderItem, OrderItemsDto>()
                .ForMember(Dest => Dest.ProductId, option => option.MapFrom(src => src.Product.ProductId))
                .ForMember(Dest => Dest.ProductName, option => option.MapFrom(src => src.Product.ProductName))
                .ForMember(Dest => Dest.PictureUrl, option => option.MapFrom<OrderPictureUrlResolver>());


            CreateMap<DeliveryMethod, DeliveryMethodeDto>();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
