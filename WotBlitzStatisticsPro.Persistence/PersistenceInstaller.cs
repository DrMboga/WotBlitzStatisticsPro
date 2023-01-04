using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SqliteWasmHelper;
using WotBlitzStatisticsPro.Persistence.DataContext;

namespace WotBlitzStatisticsPro.Persistence
{
    public static class PersistenceInstaller
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSqliteWasmDbContextFactory<WotBlitzStatisticsProContext>(
                    opts => {
                        opts.UseSqlite("Data Source=wotblitzstatisticspro.sqlite3");
                        opts.EnableSensitiveDataLogging();
                    });
        }
    }
}