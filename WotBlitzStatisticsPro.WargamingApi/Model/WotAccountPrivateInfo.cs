using System.Text.Json.Serialization;

namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotAccountPrivateInfo
    {
        ///<summary>
		/// Account ban info
		///</summary>
		[JsonPropertyName("ban_info")]
		public string? BanInfo { get; set; }

		///<summary>
		/// Account ban expiration date
		///</summary>
		[JsonPropertyName("ban_time")]
		public int? BanTime { get; set; }

		///<summary>
		/// Total time in battles until destroy in seconds
		///</summary>
		[JsonPropertyName("battle_life_time")]
		public int? BattleLifeTimeInSeconds { get; set; }

		///<summary>
		/// Amount of credits
		///</summary>
		[JsonPropertyName("credits")]
		public long? Credits { get; set; }

		///<summary>
		/// Free experience
		///</summary>
		[JsonPropertyName("free_xp")]
		public long? FreeXp { get; set; }

		///<summary>
		///Amount of gold
		///</summary>
		[JsonPropertyName("gold")]
		public long? Gold { get; set; }

		///<summary>
		/// Is player has premium account
		///</summary>
		[JsonPropertyName("is_premium")]
		public bool IsPremium { get; set; }

		///<summary>
		/// Premium account expiration time
		///</summary>
		[JsonPropertyName("premium_expires_at")]
		private int? PremiumExpiresAt { get; set; }

		///<summary>
		/// Group of contacts.
		///</summary>
		[JsonPropertyName("grouped_contacts")]
		public WotAccountPrivateInfoGroupedContacts? GroupedContacts { get; set; }

		///<summary>
		/// Account restrictions
		///</summary>
		[JsonPropertyName("restrictions")]
		public WotAccountPrivateInfoRestrictions? Restrictions { get; set; }

    }
}