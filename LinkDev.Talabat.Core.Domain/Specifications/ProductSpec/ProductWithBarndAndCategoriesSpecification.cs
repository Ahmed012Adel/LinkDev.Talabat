using LinkDev.Talabat.Core.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.ProductSpec
{
    public class ProductWithBarndAndCategoriesSpecification : BaseSpecificatins<Product , int>
    {
        public ProductWithBarndAndCategoriesSpecification(string? Sort, int? BrandId, int? CategoryId,int PageSize,int PageIndex) :
            base(
                
                p=>
                (!BrandId.HasValue || BrandId.Value == p.BrandId)
                &&
                (!CategoryId.HasValue || CategoryId.Value == p.CategoryId)

                )
        {
            AddIncludes();
            AddOrderBy(P => P.Name);
            
            if (!string.IsNullOrWhiteSpace(Sort))
            {
                switch (Sort)
                {
                    case "nameDesc":
                        AddOrderByDesc(p => p.Name);
                        break;
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }

            AddPagination(PageSize * (PageIndex - 1), PageSize);
        }
        public ProductWithBarndAndCategoriesSpecification(int id):base(id)
        {
            AddIncludes();
        }
        private protected override void AddIncludes()
        {
            base.AddIncludes();

            Includes.Add(P => P.Category!);
            Includes.Add(p => p.Brand!);
        }

    }
}
