using Microsoft.EntityFrameworkCore;
using SqliteWasmHelper;
using WotBlitzStatisticsPro.Persistence.DataContext;

namespace WotBlitzStatisticsPro.Persistence.Services
{
    public class DataAccess :
        INotificationHandler<ResetVehicleDictionariesNotification>,
        INotificationHandler<UpdateStateNotification>
    {
        private readonly ISqliteWasmDbContextFactory<WotBlitzStatisticsProContext> _contextFactory;

        public DataAccess(ISqliteWasmDbContextFactory<WotBlitzStatisticsProContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Handle(ResetVehicleDictionariesNotification notification, CancellationToken cancellationToken)
        {
            await RemoveAllVehicles();
            using var dbContext = await _contextFactory.CreateDbContextAsync();

            // Insert new vehicles dictionary
            await dbContext.AddRangeAsync(notification.Vehicles);

            await dbContext.SaveChangesAsync();
            Console.WriteLine("Vehicle dictionaries update handler");
        }

        public async Task Handle(UpdateStateNotification notification, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();

            var state = await dbContext.State.FirstOrDefaultAsync();

            if(state == null)
            {
                state = new State {
                    DictionariesLanguage = notification.Locale,
                    GameVersion = notification.GameVersion,
                    DictionariesUpdated = notification.DictionariesUpdated
                };
                await dbContext.AddAsync(state);
            }
            else
            {
                state.DictionariesLanguage = notification.Locale;
                state.GameVersion = notification.GameVersion;
                state.DictionariesUpdated = notification.DictionariesUpdated;
            }

            await dbContext.SaveChangesAsync();

            Console.WriteLine("State update handler");
        }

        private async Task RemoveAllVehicles()
        {
            // Clear vehicles, NextVehicle and VehicleModule tables
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            var allVehicles = await dbContext.VehiclesDictionary.ToListAsync();
            dbContext.RemoveRange(allVehicles);
        }
    }
}