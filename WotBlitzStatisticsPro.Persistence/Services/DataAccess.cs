using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SqliteWasmHelper;
using WotBlitzStatisticsPro.Persistence.DataContext;

namespace WotBlitzStatisticsPro.Persistence.Services
{
    public class DataAccess :
        INotificationHandler<ResetVehicleDictionariesNotification>,
        INotificationHandler<UpdateStateNotification>,
        IRequestHandler<ReadStateRequest, State>,
        IRequestHandler<GetVehiclesDictionaryRequest, DictionaryVehicle[]>,
        IRequestHandler<GetVehiclesByIdsRequest, DictionaryVehicle[]>,
        IRequestHandler<GetVehiclesCountByTierRequest, int>,
        IRequestHandler<GetVehiclesByNationRequest, DictionaryVehicle[]>
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

        public async Task<State> Handle(ReadStateRequest request, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            var state = await dbContext.State.FirstOrDefaultAsync();

            if(state == null)
            {
                return new State
                {
                    DictionariesLanguage = "En",
                    GameVersion = "0.0.0",
                    DictionariesUpdated = new DateTime(1970, 1, 1)
                };
            }
            return state;
        }

        public async Task<DictionaryVehicle[]> Handle(GetVehiclesDictionaryRequest request, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();

            return await dbContext.VehiclesDictionary.AsNoTracking()
                            .Include(v => v.VehicleModulesRelation) // .Where(rel => rel != null)
                            .ThenInclude(r => r.Module)
                            .Include(v => v.NextVehicles)
                            .ToArrayAsync();
        }

        public async Task<DictionaryVehicle[]> Handle(GetVehiclesByIdsRequest request, CancellationToken cancellationToken)
        {
            if(request.TankIds == null)
            {
                return new DictionaryVehicle[0];
            }
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            return await dbContext.VehiclesDictionary.AsNoTracking()
                    .Where(v => request.TankIds.Contains(v.TankId))
                    .ToArrayAsync();
        }

        public async Task<int> Handle(GetVehiclesCountByTierRequest request, CancellationToken cancellationToken)
        {

            Expression<Func<DictionaryVehicle, bool>> lambda;
            if(request.IsPremium.HasValue)
            {
                lambda = v => v.Tier == request.Tier && v.IsPremium == request.IsPremium.Value;
            }
            else
            {
                lambda = v => v.Tier == request.Tier;
            }
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            return await dbContext.VehiclesDictionary.AsNoTracking()
                    .Where(lambda)
                    .CountAsync();
        }

        public async Task<DictionaryVehicle[]> Handle(GetVehiclesByNationRequest request, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            return await dbContext.VehiclesDictionary.AsNoTracking()
                    .Include(v => v.NextVehicles)
                    .Where(v => request.Nation == v.Nation)
                    .ToArrayAsync();
        }

        private async Task RemoveAllVehicles()
        {
            // Clear vehicles, NextVehicle and VehicleModule tables
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            var allVehicles = await dbContext.VehiclesDictionary.ToListAsync();
            dbContext.RemoveRange(allVehicles);

            var allNextVehicles = await dbContext.VehiclesTreeDictionary.ToListAsync();
            dbContext.RemoveRange(allNextVehicles);

            var allModuleRelations = await dbContext.VehicleModulesDictionaryRelation.ToListAsync();
            dbContext.RemoveRange(allModuleRelations);

            var allModules = await dbContext.VehicleModulesDictionary.ToListAsync();
            dbContext.RemoveRange(allModules);

            await dbContext.SaveChangesAsync();
        }
    }
}