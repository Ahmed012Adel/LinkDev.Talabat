using LinkDev.Talabat.Core.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.ProductSpec
{
    public class ProductWithFilterationForCountSpecification : BaseSpecificatins<Product , int>
    {
        public ProductWithFilterationForCountSpecification(int? BrandId, int? CategoryId) :
            base(
                p =>
                (!BrandId.HasValue || BrandId.Value == p.BrandId)
                &&
                (!CategoryId.HasValue || CategoryId.Value == p.CategoryId))
        {
            
        }
    }
}
