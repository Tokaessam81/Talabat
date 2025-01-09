using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;

namespace Talabat.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _Database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _Database=redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
           return  await _Database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var basket =await _Database.StringGetAsync(BasketId);
            return  basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var CreatOrUpdateBasket = await _Database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (CreatOrUpdateBasket is false) 
                return null;
            return await GetBasketAsync(basket.Id);

        }
    }
}
