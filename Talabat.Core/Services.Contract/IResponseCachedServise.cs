using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services.Contract
{
    public interface IResponseCachedServise
    {
        Task CacheResponseAsync(string CacheKey, object Response, TimeSpan ExpireDate);
        Task<string?> GetCachedResponce(string CacheKey);
    }
}
