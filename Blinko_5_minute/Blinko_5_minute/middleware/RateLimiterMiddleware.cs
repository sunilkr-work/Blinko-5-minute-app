using Blinko_5_minute.model;
using Microsoft.Extensions.Caching.Memory;

namespace Blinko_5_minute.middleware
{
    public class RateLimiterMiddleware
    {
        private readonly int maxlimit;
        private RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _timeout;

        public RateLimiterMiddleware(int maxlimit, RequestDelegate next, TimeSpan timeout, IMemoryCache memoryCache)
        {
            this.maxlimit = maxlimit;
            _next = next;
            _timeout = timeout;
            _cache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientKey = context.Connection.RemoteIpAddress?.ToString()??"";
            var counter = _cache.GetOrCreate(clientKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _timeout;
                return new RequestCounter { Count = 0 , RetryTimeInSec = (int) _timeout.TotalSeconds};

            });

            if(counter.Count> maxlimit)
            {
                context.Response.StatusCode = 429;
                context.Response.Headers["Retry-After"] = _timeout.TotalSeconds.ToString();
                await context.Response.WriteAsync("Too Many Requests. Try again later.");
                return;
            }

            counter.Count++;
            await _next(context);

        }



      
    }
}
