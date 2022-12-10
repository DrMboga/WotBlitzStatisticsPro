namespace WotBlitzStatisticsPro.WargamingApi
{
    public class WargamingApiInstaller
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IWargamingClient, WargamingClient>();
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}