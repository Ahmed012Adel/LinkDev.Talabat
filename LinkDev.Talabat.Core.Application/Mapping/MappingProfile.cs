﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Employee.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Product.Models;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
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
        }
    }
}
