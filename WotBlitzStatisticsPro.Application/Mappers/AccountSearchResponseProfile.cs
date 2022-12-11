using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Helpers;
using WotBlitzStatisticsPro.WargamingApi.Model;

namespace WotBlitzStatisticsPro.Application.Mappers
{
    public static class AccountSearchResponseProfile
    {
        public static ShortPlayerInfoDto MapToShortPlayerInfoDto(this WotAccountListResponse wotAccountInfoResponse)
        {
            return new ShortPlayerInfoDto {
                AccountId = wotAccountInfoResponse?.AccountId ?? 0,
                Nickname = wotAccountInfoResponse?.Nickname ?? string.Empty
            };
        }

        public static ShortPlayerInfoDto MapToShortPlayerInfoDto(this ShortPlayerInfoDto shortPlayerInfo, WotAccountInfo wotAccountInfo)
        {
            long battles = wotAccountInfo?.Statistics?.All?.Battles ?? 0;
            
            shortPlayerInfo.CreatedAt = wotAccountInfo?.CreatedAt.ToDateTime() ?? new DateTime(1970,1,1);
            shortPlayerInfo.Battles = wotAccountInfo?.Statistics?.All?.Battles ?? 0;
            shortPlayerInfo.LastBattle = wotAccountInfo?.LastBattleTime.ToDateTime() ?? new DateTime(1970,1,1);
            shortPlayerInfo.WinRate = battles == 0 ? 0 : Convert.ToInt32(100 * (wotAccountInfo?.Statistics?.All?.Wins ?? 0) / battles);
            
            return shortPlayerInfo;
        }
    }
}