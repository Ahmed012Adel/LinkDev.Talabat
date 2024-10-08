using LinkDev.Talabat.Apis.Controller.Controllers.BaseController;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Product.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Apis.Controller.Controllers.Product
{
    public class ProductController(IServiceManager serviceManager) : ApiControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProductsAsync()
        {
            var Product = await serviceManager.ProductService.GetProductsAsync();
            return Ok(Product);
        }
        
        [HttpGet ("{id}")]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProductAsync(int id)
        {
            var Product = await serviceManager.ProductService.GetProductAsync(id);
            if(Product == null) return NotFound();
            return Ok(Product);
        }

        [HttpGet ("Brand")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync()
        {
            var Brand = await serviceManager.ProductService.GetBrandsAsync();
            return Ok(Brand);
        }
        
        [HttpGet ("Categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesAsync()
        {
            var Categories = await serviceManager.ProductService.GetCategoriesAsync();
            return Ok(Categories);
        }


    }
}
