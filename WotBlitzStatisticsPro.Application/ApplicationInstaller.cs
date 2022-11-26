using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WotBlitzStatisticsPro.Application.Messages;
using WotBlitzStatisticsPro.WargamingApi;
using WotBlitzStatisticsPro.WargamingApi.Messages;

namespace WotBlitzStatisticsPro.Application
{
    public static class ApplicationInstaller
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            WargamingApiInstaller.ConfigureServices(services);
        }

        public static List<Assembly> GetAllMediatRAssemblies()
        {
            return new List<Assembly> 
            { 
                typeof(FindPlayersRequest).Assembly,
                typeof(GetAccountsListRequest).Assembly
            };
        }
    }
}