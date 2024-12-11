using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Infrastructure;
using LinkDev.Talabat.Core.Application.Abstraction.Order;
using LinkDev.Talabat.Core.Application.Abstraction.Order.Models;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Specifications.OrderSpecifications;

namespace LinkDev.Talabat.Core.Application.Services.Order
{
    internal class OrderService(IBasketService basketService, IUniteOfWork uniteOfWork, IMapper mapper , IPaymentService paymentService) : IOrdersService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(string BuyerEmail, CreateOrderDto order)
        {
            // 1. get Basket From Basket Repo 
            var basket = await basketService.GetcustomerBasketAsync(order.BasketId);

            // 2. get selected Items at Basket from Product Repo

            var OrderItems = new List<OrderItem>();
            if (basket.Items.Count() > 0)
            {
                var productRepo = uniteOfWork.GetRepoitery<Domain.Entities.Product.Product, int>();

                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);

                    if (product is not null)
                    {
                        var productItem = new ProductItemOrder()
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            PictureUrl = product.PictureUrl ?? ""

                        };

                        var orderItem = new OrderItem()
                        {
                            Product = productItem,
                            Price = product.Price,
                            Quanity = item.Quantity
                        };

                        OrderItems.Add(orderItem);

                    }
                }
            }

            // 3. calculate SubTotal
            var subtotal = OrderItems.Sum(Item => Item.Price * Item.Quanity);

            // 4. Map Address 

            var Address = mapper.Map<Address>(order.ShippingAddress);

            // 5. createOrder

            var orderRepo = uniteOfWork.GetRepoitery<Domain.Entities.Orders.Order, int>();

            var spec = OrderSpec.PaymentIntent(basket.PaymentIntentId!);  

            var orderwithSpecPayment = await orderRepo.GetWithSpecAsync(spec);

            if(orderwithSpecPayment is not null)
            {
                orderRepo.Delete(orderwithSpecPayment);
                await paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }

            var OrderToCreateDto = new Domain.Entities.Orders.Order()
            {
                BuyerEmail = BuyerEmail,
                ShippingAddress = Address,
                OrderItems = OrderItems,
                SupTotal = subtotal,
                deliveryMethodId = order.DeliveryMethodId,
                PaymentIntenedId = basket.PaymentIntentId!
                
            };

            await uniteOfWork.GetRepoitery<Domain.Entities.Orders.Order, int>().AddAsync(OrderToCreateDto);
            // 6. save to database

            var Created = await uniteOfWork.ComplateAsync() > 0;

            if (!Created) throw new BadRequestException("something error occured");

            return mapper.Map<OrderToReturnDto>(OrderToCreateDto);

        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {

        var spec = new OrderSpec(buyerEmail, orderId);
        var order = await uniteOfWork.GetRepoitery<Domain.Entities.Orders.Order, int>().GetWithSpecAsync(spec);
        
            if (order is null) throw new NotFoundException(nameof(order), orderId);
            return mapper.Map<OrderToReturnDto>(order);
        }


        public async Task<IEnumerable<OrderToReturnDto>> GetOrderForUserAsync(string buyerEmail)
        {
            var spec = OrderSpec.BuyerEmail(buyerEmail);
            var orders = await uniteOfWork.GetRepoitery<Domain.Entities.Orders.Order, int>().GetWithSpecAllAsync(spec);

            return mapper.Map<IEnumerable<OrderToReturnDto>>(orders);

        }
        public async Task<IEnumerable<DeliveryMethodeDto>> GetDeliveryMethodAsync()
    {
        var deliveryMethod = await uniteOfWork.GetRepoitery<DeliveryMethod, int>().GetAllAsync();

        return mapper.Map<IEnumerable<DeliveryMethodeDto>>(deliveryMethod);
    }
}
}