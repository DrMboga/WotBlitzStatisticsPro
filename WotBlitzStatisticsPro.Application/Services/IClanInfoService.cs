namespace WotBlitzStatisticsPro.Application.Services
{
    public interface IClanInfoService
    {
        Task<ClanInfoDto?> GetClanInfo(long accountId, RequestLanguage language);
    }
}