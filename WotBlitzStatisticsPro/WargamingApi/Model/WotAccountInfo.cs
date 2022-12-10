namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotAccountInfo
    {
        ///<summary>
		/// Player account identifier
		///</summary>
		[JsonPropertyName("account_id")]
		public long AccountId { get; set; }

		///<summary>
		/// Account creation date
		///</summary>
		[JsonPropertyName("created_at")]
		public int CreatedAt { get; set; }

		///<summary>
		/// Last battle time
		///</summary>
		[JsonPropertyName("last_battle_time")]
		public int LastBattleTime { get; set; }

		///<summary>
		/// Player's nick
		///</summary>
		[JsonPropertyName("nickname")]
		public string? Nickname { get; set; }

		///<summary>
		/// Player information update date
		///</summary>
		[JsonPropertyName("updated_at")]
		public int UpdatedAt { get; set; }

		///<summary>
		/// Private account data
		///</summary>
		[JsonPropertyName("private")]
		public WotAccountPrivateInfo? Private { get; set; }

		///<summary>
		/// Player's statistics
		///</summary>
		[JsonPropertyName("statistics")]
		public WotAccountStatistics? Statistics { get; set; }

    }
}