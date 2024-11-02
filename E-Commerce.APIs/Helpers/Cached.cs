using E_Commerce.Core.Services.Contract;
using E_Commerce.Services.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.APIs.Helpers
{
    public class Cached : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;

        public Cached(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var Cachekey = GenerateCachKeyFromRequest(context.HttpContext.Request);
            var CacheResponse = await CacheService.GetCacheResponseAsync(Cachekey);
            if (!string.IsNullOrEmpty(CacheResponse))
            {
                context.Result = new ContentResult()
                {
                    Content = CacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                return;
            }
            var executEndpointExcute = await next.Invoke();
            if (executEndpointExcute.Result is OkObjectResult result)
            {
                await CacheService.CacheResponseAsync(Cachekey, result.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }


        }

        private string GenerateCachKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path);
            foreach (var (key, value) in request.Query)
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
