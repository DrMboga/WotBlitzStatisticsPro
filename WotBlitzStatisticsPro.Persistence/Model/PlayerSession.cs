namespace WotBlitzStatisticsPro.Persistence.Model
{
    public class PlayerSession
    {
        public long PlayerSessionId { get; set; }
        public long AccountId { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public DateTime LastBattleTime { get; set; } = new DateTime(1970, 1, 1);

        public long Battles { get; set; }
        public double AvgTier { get; set; }

        public decimal WinRate { get; set; }

        public decimal AvgDamage { get; set; }

        public decimal AvgXp { get; set; }

        public decimal DamageCoefficient { get; set; }

        public decimal SurvivalRate { get; set; }

        public double Wn7 { get; set; }

        public List<PlayerTankSession>? TanksSession { get; set; }
    }
}