using E_Commerce.Core.Entities.Basket;
using E_Commerce.Core.Repositories.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database=redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }
        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }
        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var serialized = JsonSerializer.Serialize(basket);
            var UpdateCreateBasket = await _database.StringSetAsync(basket.Id, serialized, TimeSpan.FromDays(7));
            return UpdateCreateBasket ? await GetBasketAsync(basket.Id) : null;
        }
    }
}
