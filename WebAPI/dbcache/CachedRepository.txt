using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.dbcache
{
    public class CachedRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;
        private readonly string _cacheKey;

        public CachedRepository(DbContext context, IMemoryCache cache,
            MemoryCacheEntryOptions cacheOptions = null)
        {
            _context = context;
            _cache = cache;
            _cacheOptions = cacheOptions ?? new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };
            _cacheKey = $"DbSet_{typeof(T).Name}";
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (!_cache.TryGetValue(_cacheKey, out IEnumerable<T> cachedData))
            {
                cachedData = await _context.Set<T>().AsNoTracking().ToListAsync();
                _cache.Set(_cacheKey, cachedData, _cacheOptions);
            }
            return cachedData;
        }

        public void InvalidateCache()
        {
            _cache.Remove(_cacheKey);
        }
    }
}
