namespace WotBlitzStatisticsPro.Application.Dto
{
    public record SessionInfoDto(
        DateTime PreviousLastBattle,
        long BattlesDiff,
        decimal AvgTierDiff,
        decimal WinRateDiff,
        decimal AvgDamageDiff,
        decimal AvgXpDiff,
        decimal DamageCoefficientDiff,
        decimal SurvivalRateDiff,
        decimal Wn7Diff
    );
}