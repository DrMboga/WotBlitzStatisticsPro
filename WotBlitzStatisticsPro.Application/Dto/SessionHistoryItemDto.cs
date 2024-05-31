namespace WotBlitzStatisticsPro.Application;

public record SessionHistoryItemDto(
    DateTime BattleTime,
    long Battles,
    decimal WinRate,
    decimal AvgDamage,
    decimal AvgXp,
    decimal Wn7
);
