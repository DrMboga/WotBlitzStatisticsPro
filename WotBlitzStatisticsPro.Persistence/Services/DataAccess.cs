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
        IRequestHandler<GetVehiclesByNationRequest, DictionaryVehicle[]>,
        IRequestHandler<GetLastPlayerSessionDateRequest, DateTime?>,
        INotificationHandler<InsertNewPlayersSessionNotification>,
        IRequestHandler<GetLastTwoPlayerSessionsRequest, PlayerSession[]?>,
        IRequestHandler<GetLastTwoTankSessionRequest, PlayerTankSession[]?>,
        INotificationHandler<UpdateLoginInfoNotification>,
        INotificationHandler<ClearAuthStateNotification>,
        IRequestHandler<GetPlanningByAccountId, List<ResourcePlanning>?>,
        INotificationHandler<AddNewPlanningNotification>,
        INotificationHandler<UpdatePlanBoughtMarkNotification>,
        IRequestHandler<GetPlayerHistoryDbRequest, PlayerSession[]?>,
        IRequestHandler<GetTankHistoryDbRequest, PlayerTankSession[]?>
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

        public async Task<DateTime?> Handle(GetLastPlayerSessionDateRequest request, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            return await dbContext.PlayerSessions.AsNoTracking()
                    .Where(s => s.AccountId == request.AccountId)
                    .OrderByDescending(s => s.LastBattleTime)
                    .Select(s => s.LastBattleTime)
                    .FirstOrDefaultAsync();
        }

        public async Task Handle(InsertNewPlayersSessionNotification notification, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();

            // Insert new vehicles dictionary
            await dbContext.AddAsync(notification.PlayerSession);

            await dbContext.SaveChangesAsync();

        }

        public Task<PlayerSession[]?> Handle(GetLastTwoPlayerSessionsRequest request, CancellationToken cancellationToken)
        {
            return Handle(new GetPlayerHistoryDbRequest(request.AccountId, 2), cancellationToken);
        }

        public Task<PlayerTankSession[]?> Handle(GetLastTwoTankSessionRequest request, CancellationToken cancellationToken)
        {
            return Handle(new GetTankHistoryDbRequest(request.AccountId, request.TankId, 2), cancellationToken);
        }

        public async Task Handle(UpdateLoginInfoNotification notification, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();

            var state = await dbContext.State.FirstOrDefaultAsync();

            if(state == null)
            {
                state = new State {
                    LoggedInAccountId = notification.AccountId,
                    WgToken = notification.AuthToken,
                    WgTokenExpiration = notification.Expiration
                };
                await dbContext.AddAsync(state);
            }
            else
            {
                state.LoggedInAccountId = notification.AccountId;
                state.WgToken = notification.AuthToken;
                state.WgTokenExpiration = notification.Expiration;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task Handle(ClearAuthStateNotification notification, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();

            var state = await dbContext.State.FirstOrDefaultAsync();

            if(state != null)
            {
                state.LoggedInAccountId = null;
                state.WgToken = null;
                state.WgTokenExpiration = null;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<List<ResourcePlanning>?> Handle(GetPlanningByAccountId request, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            return await dbContext.ResourcePlanning.AsNoTracking()
                    .Where(p => p.AccountId == request.AccountId)
                    .ToListAsync();
        }

        public async Task Handle(AddNewPlanningNotification notification, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            await dbContext.ResourcePlanning.AddAsync(new ResourcePlanning{
                AccountId = notification.AccountId,
                TankId = notification.TankId,
                Order = notification.Order,
                SaleCert = notification.SaleCert,
                PlanningEquipment = notification.PlanningEquipment
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task Handle(UpdatePlanBoughtMarkNotification notification, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            var plan = await dbContext.ResourcePlanning
                    .Where(p => p.AccountId == notification.AccountId && p.TankId == notification.TankId)
                    .FirstOrDefaultAsync();
            if (plan != null)
            {
                plan.Bought = notification.BuyDate;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<PlayerSession[]?> Handle(GetPlayerHistoryDbRequest request, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            return await dbContext.PlayerSessions.AsNoTracking()
                                .Where(s => s.AccountId == request.AccountId)
                                .OrderByDescending(s => s.LastBattleTime)
                                .Take(request.Take)
                                .ToArrayAsync();
        }

        public async Task<PlayerTankSession[]?> Handle(GetTankHistoryDbRequest request, CancellationToken cancellationToken)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            return await dbContext.PlayerTankSessions.AsNoTracking()
                                .Where(s => s.AccountId == request.AccountId && s.TankId == request.TankId)
                                .OrderByDescending(s => s.LastBattleTime)
                                .Take(request.Take)
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