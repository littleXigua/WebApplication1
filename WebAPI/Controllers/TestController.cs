using EasyCaching.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly IEasyCachingProvider _cache;

        public TestController(IEasyCachingProvider cache) => _cache = cache;

        [HttpGet]
        public async Task<string> Get()
        {
            var cacheKey = "test_key";
            var value = await _cache.GetAsync<string>(cacheKey);

            if (!value.HasValue)
            {
                await _cache.SetAsync(cacheKey, "Hello .NET 8", TimeSpan.FromMinutes(1));
                return "Value was set";
            }

            return $"Cached value: {value.Value}";
        }
    }
}
