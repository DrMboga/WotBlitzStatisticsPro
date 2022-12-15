namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class ClanAccountInfo
    {
        ///<summary>
		/// AccountId
		///</summary>
		[JsonPropertyName("account_id")]
		public long? AccountId { get; set; }

		///<summary>
		/// Account nick
		///</summary>
		[JsonPropertyName("account_name")]
		public string? AccountName { get; set; }

		///<summary>
		/// ClanId
		///</summary>
		[JsonPropertyName("clan_id")]
		public long? ClanId { get; set; }

		///<summary>
		/// Date of joining clan
		///</summary>
		[JsonPropertyName("joined_at")]
		public int? JoinedAt { get; set; }

		///<summary>
		/// Player clan role
		///</summary>
		[JsonPropertyName("role")]
		public string? Role { get; set; }

    }
}