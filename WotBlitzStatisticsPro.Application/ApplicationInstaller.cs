using Microsoft.Extensions.DependencyInjection;
using WotBlitzStatisticsPro.Application.Mocks;
using WotBlitzStatisticsPro.Application.Services;
using WotBlitzStatisticsPro.WargamingApi;

namespace WotBlitzStatisticsPro.Application
{
    public static class ApplicationInstaller
    {
        public static void ConfigureServices(IServiceCollection services, Boolean useMockData)
        {
            if (useMockData)
            {
                ConfigureMocks(services);
            }
            else
            {
                services.AddTransient<IFindPlayersService, FindPlayersService>();
                services.AddTransient<IPlayerInfoService, PlayerInfoService>();
                WargamingApiInstaller.ConfigureServices(services);
            }
        }

        private static void ConfigureMocks(IServiceCollection services)
        {
            services.AddTransient<IFindPlayersService, FindPlayersServiceMock>();
            services.AddTransient<IPlayerInfoService, PlayerInfoServiceMock>();
        }
    }
}