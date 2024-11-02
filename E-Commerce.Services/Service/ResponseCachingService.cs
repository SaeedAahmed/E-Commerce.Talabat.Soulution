using E_Commerce.Core.Services.Contract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Services.Service
{
    public class ResponseCachingService : IResponseCacheService
    {
        private readonly IDatabase _database;
        public ResponseCachingService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();

        }
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null) return;
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var SerializedResponse = JsonSerializer.Serialize(response, options);
            await _database.StringSetAsync(cacheKey, SerializedResponse);
        }

        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
            var response = await _database.StringGetAsync(cacheKey);
            if (response.IsNullOrEmpty) return null;
            return response;
        }
    }
}
