using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Services.Contract;

namespace Talabat.Services
{
    public class ResponseCachedServise : IResponseCachedServise
    {
        private readonly IDatabase _db;
        public ResponseCachedServise(IConnectionMultiplexer Redis)
        {
            _db=Redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string CacheKey, object Response, TimeSpan ExpireDate)
        {
            if (Response == null) 
               return;
            var option = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var ResponseSerelizer=JsonSerializer.Serialize(Response, option);
           await _db.StringSetAsync(CacheKey, ResponseSerelizer, ExpireDate);

        }

        public async Task<string?> GetCachedResponce(string CacheKey)
        {
        var response= await _db.StringGetAsync(CacheKey);
            if(response.IsNullOrEmpty)return null;
            return response;
        }
    }
}
