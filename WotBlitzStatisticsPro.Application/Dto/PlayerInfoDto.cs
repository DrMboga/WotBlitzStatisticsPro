using WotBlitzStatisticsPro.Model;

namespace WotBlitzStatisticsPro.Application.Dto
{
    public record PlayerInfoDto
    (
        long AccountId,
        string Nickname,
        DateTimeOffset CreatedAt,
        long Battles,
        DateTimeOffset LastBattle,
        long? MaxFragsTankId,
        long? MaxXpTankId,
        string? ClanTag,
        int WinRate,
        double? AvgTier
    ): IPlayerInfo;
}