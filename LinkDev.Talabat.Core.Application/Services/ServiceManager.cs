using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Product;
using LinkDev.Talabat.Core.Application.Services.Product;
using LinkDev.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        public ServiceManager(IUniteOfWork uniteOfWork ,IMapper mapper)
        {

            _productService = new Lazy<IProductService>(() => new ProductService(uniteOfWork,mapper));
        }
        public IProductService ProductService => _productService.Value;
     
    }
}
