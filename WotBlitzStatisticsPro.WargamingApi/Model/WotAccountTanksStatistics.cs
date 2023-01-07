namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotAccountTanksStatistics
    {
        ///<summary>
        /// Player account identifier
        ///</summary>
        [JsonPropertyName("account_id")]
        public long AccountId { get; set; }

        [JsonPropertyName("battle_life_time")]
        public int BattleLifeTimeInSeconds { get; set; }

        ///<summary>
        /// Last battle
        ///</summary>
        [JsonPropertyName("last_battle_time")]
        public int LastBattleTime { get; set; }

        [JsonPropertyName("in_garage")]
        public bool? InGarage { get; set; }

        [JsonPropertyName("in_garage_updated")]
        private int? InGarageUpdated { get; set; }

        ///<summary>
        /// Mastery:
        ///
        /// 0 — None
        /// 1 — Rank 3
        /// 2 — Rank 2
        /// 3 — Rank 1
        /// 4 — Mastery
        ///</summary>
        [JsonPropertyName("mark_of_mastery")]
        public long MarkOfMastery { get; set; }

        ///<summary>
        /// Tank identifier
        ///</summary>
        [JsonPropertyName("tank_id")]
        public long TankId { get; set; }

        ///<summary>
        /// Whole statistics
        ///</summary>
        [JsonPropertyName("all")]
        public WotAccountTanksFullStatistics? All { get; set; }

        [JsonPropertyName("frags")]
        public Dictionary<string, string>? Frags { get; set; }

    }

        public class WotAccountTanksFullStatistics
    {
		///<summary>
		/// Battles count
		///</summary>
		[JsonPropertyName("battles")]
		public long? Battles { get; set; }

		///<summary>
		/// Base capture points
		///</summary>
		[JsonPropertyName("capture_points")]
		public long? CapturePoints { get; set; }

		///<summary>
		/// Damage dealt
		///</summary>
		[JsonPropertyName("damage_dealt")]
		public long? DamageDealt { get; set; }

		///<summary>
		/// Damage received
		///</summary>
		[JsonPropertyName("damage_received")]
		public long? DamageReceived { get; set; }

		///<summary>
		/// Base dropped capture points
		///</summary>
		[JsonPropertyName("dropped_capture_points")]
		public long? DroppedCapturePoints { get; set; }

		///<summary>
		/// Frags count
		///</summary>
		[JsonPropertyName("frags")]
		public long? Frags { get; set; }

		///<summary>
		/// Frags count after 8 tier
		///</summary>
		[JsonPropertyName("frags8p")]
		public long? Frags8P { get; set; }

		///<summary>
		/// Hits count
		///</summary>
		[JsonPropertyName("hits")]
		public long? Hits { get; set; }

		///<summary>
		/// Losses count
		///</summary>
		[JsonPropertyName("losses")]
		public long? Losses { get; set; }

		///<summary>
		/// Max frags per battle
		///</summary>
		[JsonPropertyName("max_frags")]
		public long? MaxFrags { get; set; }

		///<summary>
		/// Max XP per battle
		///</summary>
		[JsonPropertyName("max_xp")]
		public long? MaxXp { get; set; }

		///<summary>
		/// Shots count
		///</summary>
		[JsonPropertyName("shots")]
		public long? Shots { get; set; }

		///<summary>
		/// Spotted vehicles count
		///</summary>
		[JsonPropertyName("spotted")]
		public long? Spotted { get; set; }

		///<summary>
		/// Amount of survived battles
		///</summary>
		[JsonPropertyName("survived_battles")]
		public long? SurvivedBattles { get; set; }

		///<summary>
		/// Amount of survived and wined battles
		///</summary>
		[JsonPropertyName("win_and_survived")]
		public long? WinAndSurvived { get; set; }

		///<summary>
		/// Wins count
		///</summary>
		[JsonPropertyName("wins")]
		public long? Wins { get; set; }

		///<summary>
		/// Total experience
		///</summary>
		[JsonPropertyName("xp")]
		public long? Xp { get; set; }

	}

}