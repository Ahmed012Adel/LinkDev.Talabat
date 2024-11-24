using LinkDev.Talabat.Core.Application.Abstraction.AuthService;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Order;
using LinkDev.Talabat.Core.Application.Abstraction.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get;}

        public IBasketService BasketService { get;}
        public IAuthService AuthService { get; }

        public IOrdersService OrdersService { get; }
    }
}
