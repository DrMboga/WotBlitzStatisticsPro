using WotBlitzStatisticsPro.Model;

namespace WotBlitzStatisticsPro.Application.Dto
{
    public class PlayerInfoDto : IPlayerInfo, IStatistics
    {
        public long AccountId { get; set; }

        public string Nickname { get; set; } = string.Empty;

        public DateTimeOffset CreatedAt { get; set; } = new DateTime(1970, 1, 1);

        public long? MaxFragsTankId { get; set; }

        public long? MaxXpTankId { get; set; }

        public double? AvgTier { get; set; }

        public DateTimeOffset LastBattleTime { get; set; } = new DateTime(1970, 1, 1);

        public long Battles { get; set; }

        public long CapturePoints { get; set; }

        public long DamageDealt { get; set; }

        public long DamageReceived { get; set; }

        public long DroppedCapturePoints { get; set; }

        public long Frags { get; set; }

        public long Frags8P { get; set; }

        public long Hits { get; set; }

        public long Losses { get; set; }

        public long MaxFrags { get; set; }

        public long MaxXp { get; set; }

        public long Shots { get; set; }

        public long Spotted { get; set; }

        public long SurvivedBattles { get; set; }

        public long WinAndSurvived { get; set; }

        public long Wins { get; set; }

        public long Xp { get; set; }

        public double Wn7 { get; set; }

        public decimal WinRate  => Battles == 0 ? 0m : (decimal)100 * Wins / Battles;

        public decimal AvgDamage => Battles == 0 ? 0m : (decimal)DamageDealt / Battles;

        public decimal AvgXp => Battles == 0 ? 0m : (decimal)Xp / Battles;

        public decimal DamageCoefficient => DamageReceived == 0 ? 0m : (decimal)DamageDealt / DamageReceived;

        public decimal SurvivalRate => Battles == 0 ? 0m : (decimal)100 * SurvivedBattles / Battles;
    }
}