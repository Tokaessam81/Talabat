using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Services.Contract;

namespace TalabatAPI.Helpers
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expireTimeForSecond;

        public CacheAttribute(int ExpireTimeForSecond)
        {
            _expireTimeForSecond = ExpireTimeForSecond;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheServices=context.HttpContext.RequestServices.GetRequiredService<IResponseCachedServise>();
            var CacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var CacheResponse = await CacheServices.GetCachedResponce(CacheKey);
            if (CacheResponse != null)
            {
                var contentResult = new ContentResult()
                {
                    Content = CacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }
            var ExecuteEndpointContext= await next.Invoke();
            if (ExecuteEndpointContext.Result is OkObjectResult result)
            {
                await CacheServices.CacheResponseAsync(CacheKey, result.Value,TimeSpan.FromSeconds(_expireTimeForSecond));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var KeyBuilder=new StringBuilder();
            KeyBuilder.Append(request.Path);
            foreach (var (Key, Value) in request.Query.OrderBy(k=>k.Key))
            {
                KeyBuilder.Append($"|{Key}-{Value}");
            }
            return KeyBuilder.ToString();
        }
    }
}
