namespace WotBlitzStatisticsPro.Application.Services
{
    public interface IPlayerInfoService
    {
        Task<PlayerInfoDto> GetFullPlayerStatistics(long accountId, RequestLanguage language);
        Task<PlayerPrivateInfoDto> GetPlayerPrivateInfo(long accountId, RequestLanguage language, string accessToken);
    }
}