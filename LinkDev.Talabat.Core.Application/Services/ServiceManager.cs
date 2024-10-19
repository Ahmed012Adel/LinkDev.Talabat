using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.AuthService;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
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

        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthService> _authService;
        public ServiceManager(IUniteOfWork uniteOfWork ,IMapper mapper,Func<IBasketService> basketServiceFactory ,Func<IAuthService> AuthFactory)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(uniteOfWork,mapper));
            _basketService = new Lazy<IBasketService>(basketServiceFactory,LazyThreadSafetyMode.ExecutionAndPublication);
            _authService = new Lazy<IAuthService>(AuthFactory, LazyThreadSafetyMode.ExecutionAndPublication);
        }
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthService AuthService => _authService.Value;
    }
}
