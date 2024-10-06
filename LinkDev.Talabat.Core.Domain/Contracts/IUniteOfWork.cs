using LinkDev.Talabat.Core.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface IUniteOfWork : IAsyncDisposable
    {
        public IGenericRepositeries<Product,int> ProductRepositery { get;  }
        public IGenericRepositeries<ProductBrand,int> BrandRepositery { get; }
        public IGenericRepositeries<ProductCategory,int> CategoryRepositery { get;  }
    
        Task ComplateAsync();
    }
}
