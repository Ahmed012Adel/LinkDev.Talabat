using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Product;
using LinkDev.Talabat.Infrastructrure.Persistence.Data.Repositeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.UnitOfWork
{
    public class UnitOfWork_Store_ : IUniteOfWork
    {
        private readonly StoreDbContxt dbContxt;
        private readonly Lazy<IGenericRepositeries<Product, int>> _ProductRepositeries;
        private readonly Lazy<IGenericRepositeries<ProductBrand, int>> _brandRepositeries;
        private readonly Lazy<IGenericRepositeries<ProductCategory, int>> _categoryRepositeries;

        public UnitOfWork_Store_(StoreDbContxt dbContxt)
        {
            this.dbContxt = dbContxt;
            _ProductRepositeries = new Lazy<IGenericRepositeries<Product, int>>(() => new GenericRepositeries<Product, int>(dbContxt));
            _brandRepositeries = new Lazy<IGenericRepositeries<ProductBrand, int>>(() => new GenericRepositeries<ProductBrand, int>(dbContxt));
            _categoryRepositeries = new Lazy<IGenericRepositeries<ProductCategory, int>>(() => new GenericRepositeries<ProductCategory, int>(dbContxt));
        }
        public IGenericRepositeries<Product, int> ProductRepositery => _ProductRepositeries.Value;
        public IGenericRepositeries<ProductBrand, int> BrandRepositery => _brandRepositeries.Value;
            public IGenericRepositeries<ProductCategory, int> CategoryRepositery => _categoryRepositeries.Value;
        public Task ComplateAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
