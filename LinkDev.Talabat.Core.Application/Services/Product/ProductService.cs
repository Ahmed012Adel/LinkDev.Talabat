using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Product;
using LinkDev.Talabat.Core.Application.Abstraction.Product.Models;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Product
{
    internal class ProductService(IUniteOfWork uniteOfWork, IMapper mapper) : IProductService
    {
        public async Task<ProductToReturnDto> GetProductAsync(int id)
             => mapper.Map<ProductToReturnDto>(await uniteOfWork.GetRepoitery<Core.Domain.Entities.Product.Product, int>().GetAsync(id));
        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
            => mapper.Map<IEnumerable<ProductToReturnDto>>(await uniteOfWork.GetRepoitery<Core.Domain.Entities.Product.Product, int>().GetAllAsync());

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
            => mapper.Map<IEnumerable<BrandDto>>(await uniteOfWork.GetRepoitery<ProductBrand, int>().GetAllAsync());
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
            => mapper.Map<IEnumerable<CategoryDto>>(await uniteOfWork.GetRepoitery<ProductCategory, int>().GetAllAsync());

    }
}
