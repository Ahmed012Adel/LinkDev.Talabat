using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Product;
using LinkDev.Talabat.Core.Application.Abstraction.Product.Models;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using LinkDev.Talabat.Core.Domain.Specifications.ProductSpec;

namespace LinkDev.Talabat.Core.Application.Services.Product
{
    internal class ProductService(IUniteOfWork uniteOfWork, IMapper mapper) : IProductService
    {
        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductWithBarndAndCategoriesSpecification(id);
            return mapper.Map<ProductToReturnDto>(await uniteOfWork.GetRepoitery<Core.Domain.Entities.Product.Product, int>().GetWithSpecAsync(spec));
        }
        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams)
        {
            var spec = new ProductWithBarndAndCategoriesSpecification(specParams.Sort, specParams.BrandId, specParams.CategoryId,specParams.PageSize,specParams.PageIndex);

            var Products = await uniteOfWork.GetRepoitery<Domain.Entities.Product.Product, int>().GetWithSpecAllAsync(spec);
            

            var CountSpec = new ProductWithFilterationForCountSpecification(specParams.BrandId,specParams.CategoryId);

            var count = await uniteOfWork.GetRepoitery<Domain.Entities.Product.Product, int>().GetCountAsync(CountSpec);
            var ProductToReturnDto = mapper.Map<IEnumerable<ProductToReturnDto>>(Products);

            return new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, count) { Data = ProductToReturnDto };
        }
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
            => mapper.Map<IEnumerable<BrandDto>>(await uniteOfWork.GetRepoitery<ProductBrand, int>().GetAllAsync());
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
            => mapper.Map<IEnumerable<CategoryDto>>(await uniteOfWork.GetRepoitery<ProductCategory, int>().GetAllAsync());

    }
}
