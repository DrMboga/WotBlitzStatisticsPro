using Microsoft.Extensions.DependencyInjection;
using WotBlitzStatisticsPro.Application.Mocks;
using WotBlitzStatisticsPro.Persistence;
using WotBlitzStatisticsPro.WargamingApi;

namespace WotBlitzStatisticsPro.Application
{
    public static class ApplicationInstaller
    {
        public static void ConfigureServices(IServiceCollection services, Boolean useMockData, string baseAddress)
        {
            if (useMockData)
            {
                ConfigureMocks(services);
            }
            else
            {
                services.AddHttpClient<IStaticData, StaticData>(client =>
                    {
                        client.BaseAddress = new Uri(baseAddress);
                    });
                services.AddTransient<IFindPlayersService, FindPlayersService>();
                services.AddTransient<IPlayerInfoService, PlayerInfoService>();
                services.AddTransient<IClanInfoService, ClanInfoService>();
                WargamingApiInstaller.ConfigureServices(services);
                PersistenceInstaller.ConfigureServices(services);
            }
        }

        private static void ConfigureMocks(IServiceCollection services)
        {
            services.AddTransient<IFindPlayersService, FindPlayersServiceMock>();
            services.AddTransient<IPlayerInfoService, PlayerInfoServiceMock>();
            services.AddTransient<IClanInfoService, ClanInfoServiceMock>();
            services.AddTransient<IStaticData, StaticDataMock>();
        }
    }
}