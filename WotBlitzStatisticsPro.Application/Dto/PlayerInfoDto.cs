using WotBlitzStatisticsPro.Model;

namespace WotBlitzStatisticsPro.Application.Dto
{
    public record PlayerInfoDto
    (
        long AccountId,
        string Nickname,
        DateTime CreatedAt,
        long Battles,
        DateTime LastBattle,
        long? MaxFragsTankId,
        long? MaxXpTankId,
        string? ClanTag,
        int WinRate,
        double? AvgTier
    ): IPlayerInfo;
}