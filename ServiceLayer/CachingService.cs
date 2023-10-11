using CoreLayer.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IDatabase = StackExchange.Redis.IDatabase;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ServiceLayer
{
    public class CachingService : ICachingService
    {
        public readonly IDatabase redis;
        public CachingService(IConnectionMultiplexer database)
        {
           redis = database.GetDatabase();  
        }
        public async Task CreateCachedResponceAsync(string Cachedkey, object response, TimeSpan lifetime)
        {
            if (response == null) return;
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; 
            var serializedResponse = JsonSerializer.Serialize(response , options);

            var cashedResponse =await redis.StringSetAsync(Cachedkey, serializedResponse, lifetime);

        }

        public async Task<string> GetCachedResponseAsync(string Cachedkey)
        {
            var response = await redis.StringGetAsync(Cachedkey);

            if (response.IsNullOrEmpty) return null;

            return response; 
            
        }
    }
}
