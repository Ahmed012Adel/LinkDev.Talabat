using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Product.Models;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Mapping
{
    internal  class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();
            CreateMap<Product, ProductToReturnDto>();
        }
    }
}
