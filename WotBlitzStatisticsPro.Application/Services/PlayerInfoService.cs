using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Model;

namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerInfoService : IPlayerInfoService
    {
        public async Task<PlayerInfoDto> GetFullPlayerStatistics(long accountId, RequestLanguage language)
        {
            // TODO: Add implementation calling WG Api request
            return new PlayerInfoDto { Nickname = "Fake" };
        }
    }
}