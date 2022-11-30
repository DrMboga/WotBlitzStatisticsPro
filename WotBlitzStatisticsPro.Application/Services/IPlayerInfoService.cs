using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Model;

namespace WotBlitzStatisticsPro.Application.Services
{
    public interface IPlayerInfoService
    {
        Task<PlayerInfoDto> GetFullPlayerStatistics(long accountId, RequestLanguage language);
    }
}