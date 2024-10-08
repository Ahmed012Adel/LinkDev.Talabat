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
        public ProductWithBarndAndCategoriesSpecification():base()
        {
            AddIncludes();
        }

        public ProductWithBarndAndCategoriesSpecification(int id):base(id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(P => P.Category!);
            Includes.Add(p => p.Brand!);
        }
    }
}
