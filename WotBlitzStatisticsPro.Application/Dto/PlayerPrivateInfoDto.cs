namespace WotBlitzStatisticsPro.Application.Dto
{
    public class PlayerPrivateInfoDto
    {
        public long AccountId { get; set; }

        public string Nickname { get; set; } = string.Empty;
        
		public int BattleLifeTimeInSeconds { get; set; }

		public long Credits { get; set; }

		public long FreeXp { get; set; }

		public long Gold { get; set; }

		public bool IsPremium { get; set; }

		public DateTime PremiumExpiresAt { get; set; }

        public TankInfoDto[]? Tanks { get; set; }
    }
}