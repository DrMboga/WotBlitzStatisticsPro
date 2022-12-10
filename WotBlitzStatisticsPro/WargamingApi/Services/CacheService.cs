using Microsoft.Extensions.Caching.Memory;

namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public class CacheService: ICacheService
    {

        // Requests expiration time in mins
        private const int AbsoluteExpirationIntervalInMinutes = 10;

        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public string? CachedRequest(string requestUrl)
        {
            if (_memoryCache.TryGetValue(requestUrl, out string? response))        
            {            
                return response;  
            }
            return null;
        }

        public void PutToCache(string requestUrl, string result)
        {
            var options = new MemoryCacheEntryOptions()        
            {            
                AbsoluteExpirationRelativeToNow = 
                                    TimeSpan.FromMinutes(AbsoluteExpirationIntervalInMinutes),            
        
            };       
            
            _memoryCache.Set(requestUrl, result, options); 
        }
    }
}