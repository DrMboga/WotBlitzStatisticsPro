namespace WotBlitzStatisticsPro.Application.Mappers;

public static class SessionHistoryProfile
{
    public static SessionHistoryItemDto ToSessionHistoryItem(this PlayerSession playerSession)
    {
        return new SessionHistoryItemDto(
            playerSession.LastBattleTime,
            playerSession.Battles,
            playerSession.WinRate,
            playerSession.AvgDamage,
            playerSession.AvgXp,
            Convert.ToDecimal(playerSession.Wn7)
        );
    }

    public static SessionHistoryItemDto ToSessionHistoryItem(this PlayerTankSession tankSession)
    {
        return new SessionHistoryItemDto(
            tankSession.LastBattleTime,
            tankSession.Battles,
            tankSession.WinRate,
            tankSession.AvgDamage,
            tankSession.AvgXp,
            Convert.ToDecimal(tankSession.Wn7)
        );
    }
}
