using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Services;
using WotBlitzStatisticsPro.Model;

namespace WotBlitzStatisticsPro.Application.Mocks
{
    public class PlayerInfoServiceMock : IPlayerInfoService
    {
        public Task<PlayerInfoDto> GetFullPlayerStatistics(long accountId, RequestLanguage language)
        {
            return Task.FromResult(new PlayerInfoDto());
        }
    }
}