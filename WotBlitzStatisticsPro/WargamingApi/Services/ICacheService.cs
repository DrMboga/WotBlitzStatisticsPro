namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public interface ICacheService
    {
        string? CachedRequest(string requestUrl);

        void PutToCache(string requestUrl, string result);
    }

}