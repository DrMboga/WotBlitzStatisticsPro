using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WotBlitzStatisticsPro.Application.Messages;
using WotBlitzStatisticsPro.Application.Services;

namespace WotBlitzStatisticsPro.Application
{
    public static class ApplicationInstaller
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // services.AddTransient<PlayerInfoService>();
        }

        public static List<Assembly> GetAllMediatRAssemblies()
        {
            return new List<Assembly> 
            { 
                typeof(FindPlayersRequest).Assembly
            };
        }
    }
}