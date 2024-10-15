using LinkDev.Talabat.Core.Domain.Contracts.Infrustructure;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure
{
    internal class BasketGeneric : IBasketGeneric
    {
        private readonly IDatabase _database;
        public BasketGeneric(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket?> GetAsync(string id)
        {
            var Basket = await _database.StringGetAsync(id);

            return Basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }

        public async Task<CustomerBasket?> UpdateBasket(CustomerBasket basket , TimeSpan timeToLife)
        {
            var value = JsonSerializer.Serialize(basket);
            var Updated = await _database.StringSetAsync(basket.Id, value, timeToLife);
            if (Updated) 
                return basket;
            return null;
        }
        public async Task DeleteBasket(string id)=>await _database.StringGetDeleteAsync(id);

    }
}
