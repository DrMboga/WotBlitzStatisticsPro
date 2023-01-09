using System.Text.Json;
using System.Text.Json.Serialization;
namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerInfoService : IPlayerInfoService
    {
        private readonly IMediator _mediator;

        public PlayerInfoService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PlayerInfoDto> GetFullPlayerStatistics(long accountId, RequestLanguage language)
        {
            var accountStatistics = await _mediator.Send(new GetAccountStatisticsRequest(accountId, language));

            var playerInfo = accountStatistics.MapToPlayerInfoDto();

            var tanksSTatistics = await _mediator.Send(new GetTankStatisticsRequest(accountId, language));

            var tankIds = tanksSTatistics.Select(t => t.TankId).ToArray();

            var tankAchievements = await _mediator.Send(new GetTanksAchievementsRequest(accountId, tankIds, language));
            var tankAchievementsDto = tankAchievements
                                        .Where(a => a.Achievements != null && a.TankId.HasValue)
                                        .ToDictionary(a => a.TankId!.Value, a => a.Achievements.ToAchievementsDto());

            // TODO: Check dictionary last date (mediatR message)

            var vehiclesInfo = await _mediator.Send(new GetVehiclesByIdsRequest(tankIds));

            playerInfo.Tanks = tanksSTatistics.ToTankInfoDto(vehiclesInfo, tankAchievementsDto);

            playerInfo.AvgTier = playerInfo.Tanks.AvgTier(playerInfo.Battles);
            playerInfo.CalculateWn7();

            var accountAchievements = await _mediator.Send(new GetAccountAchievementsRequest(accountId, language));
            playerInfo.Achievements = accountAchievements.Achievements?.ToAchievementsDto();


            // Console.WriteLine(JsonSerializer.Serialize(playerInfo, options: new()
            // {
            //     ReferenceHandler = ReferenceHandler.Preserve
            // }));

            return playerInfo;
        }
    }
}