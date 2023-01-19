namespace WotBlitzStatisticsPro.Persistence.Model
{
    public class PlayerTankSession
    {
        public long PlayerTankSessionId { get; set; }
        public long PlayerSessionId { get; set; }
        public PlayerSession? PlayerSession { get; set; }
        public long AccountId { get; set; }
        public long TankId { get; set; }
        public DateTime LastBattleTime { get; set; } = new DateTime(1970, 1, 1);

        public long Battles { get; set; }
        public decimal WinRate { get; set; }

        public decimal AvgDamage { get; set; }

        public decimal AvgXp { get; set; }

        public decimal DamageCoefficient { get; set; }

        public decimal SurvivalRate { get; set; }

        public double Wn7 { get; set; }

    }
}