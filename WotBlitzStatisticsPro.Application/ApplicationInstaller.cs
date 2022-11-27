using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WotBlitzStatisticsPro.Application.Mappers;
using WotBlitzStatisticsPro.Application.Messages;
using WotBlitzStatisticsPro.Application.Services;
using WotBlitzStatisticsPro.WargamingApi;
using WotBlitzStatisticsPro.WargamingApi.Messages;

namespace WotBlitzStatisticsPro.Application
{
    public static class ApplicationInstaller
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AccountSearchResponseProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IFindPlayersService, FindPlayersService>();

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