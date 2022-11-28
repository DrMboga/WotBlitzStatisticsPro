using Microsoft.Extensions.DependencyInjection;
using WotBlitzStatisticsPro.WargamingApi.Services;

namespace WotBlitzStatisticsPro.WargamingApi
{
    public static class WargamingApiInstaller
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IWargamingClient, WargamingClient>();
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}