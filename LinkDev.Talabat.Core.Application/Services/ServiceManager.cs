using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Order;
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
        private readonly Lazy<IOrdersService> _orderService;
        public ServiceManager(IUniteOfWork uniteOfWork ,IMapper mapper,Func<IOrdersService> orderServiceFactory, Func<IBasketService> basketServiceFactory)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(uniteOfWork,mapper));
            _basketService = new Lazy<IBasketService>(basketServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _orderService = new Lazy<IOrdersService>(orderServiceFactory , LazyThreadSafetyMode.ExecutionAndPublication);
        }
        public IProductService ProductService => _productService.Value;
        public IBasketService BasketService => _basketService.Value;

        public IOrdersService OrdersService => _orderService.Value;

    }
}
