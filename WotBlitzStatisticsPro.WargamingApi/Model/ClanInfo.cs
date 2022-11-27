using System.Text.Json.Serialization;

namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class ClanInfo
    {
		///<summary>
		/// Clan can accept new players
		///</summary>
		[JsonPropertyName("accepts_join_requests")]
		public bool AcceptsJoinRequests { get; set; }

		///<summary>
		/// Clan identifier
		///</summary>
		[JsonPropertyName("clan_id")]
		public long? ClanId { get; set; }

		///<summary>
		/// Clan color in HEX #RRGGBB
		///</summary>
		[JsonPropertyName("color")]
		public string? Color { get; set; }

		///<summary>
		/// Date of clan creation
		///</summary>
		[JsonPropertyName("created_at")]
        public int? CreatedAt { get; set; }

		///<summary>
		/// Clan creator account id
		///</summary>
		[JsonPropertyName("creator_id")]
		public long? CreatorId { get; set; }

		///<summary>
		/// Clan creator nick
		///</summary>
		[JsonPropertyName("creator_name")]
		public string? CreatorName { get; set; }

		///<summary>
		/// Clan description
		///</summary>
		[JsonPropertyName("description")]
		public string? Description { get; set; }

		///<summary>
		/// Html Clan description 
		///</summary>
		[JsonPropertyName("description_html")]
		public string? DescriptionHtml { get; set; }

		///<summary>
		/// Is clan deleted. Actual info about deleted clan only in fields: clan_id, is_clan_disbanded, updated_at.
		///</summary>
		[JsonPropertyName("is_clan_disbanded")]
		public bool? IsClanDisbanded { get; set; }

		///<summary>
		/// Clan commander accountId
		///</summary>
		[JsonPropertyName("leader_id")]
		public long? LeaderId { get; set; }

		///<summary>
		/// Clan commander nick
		///</summary>
		[JsonPropertyName("leader_name")]
		public string? LeaderName { get; set; }

		///<summary>
		/// Clan members count
		///</summary>
		[JsonPropertyName("members_count")]
		public long? MembersCount { get; set; }

		///<summary>
		/// Clan motto
		///</summary>
		[JsonPropertyName("motto")]
		public string? Motto { get; set; }

		///<summary>
		/// Clan name
		///</summary>
		[JsonPropertyName("name")]
		public string? Name { get; set; }

		///<summary>
		/// Renamed clan name
		///</summary>
		[JsonPropertyName("old_name")]
		public string? OldName { get; set; }

		///<summary>
		/// Renamed clan tag
		///</summary>
		[JsonPropertyName("old_tag")]
		public string? OldTag { get; set; }

		///<summary>
		/// Clan renaming time in UTC
		///</summary>
		[JsonPropertyName("renamed_at")]
		public int? RenamedAt { get; set; }

		///<summary>
		/// Clan tag
		///</summary>
		[JsonPropertyName("tag")]
		public string? Tag { get; set; }

		///<summary>
		/// Clan info updated at
		///</summary>
		[JsonPropertyName("updated_at")]
		public int? UpdatedAt { get; set; }

		///<summary>
		/// Clan emblems info
		///</summary>
		[JsonPropertyName("emblems")]
		public WgnClansInfoEmblems? Emblems { get; set; }

		///<summary>
		/// Clan members information. Depends on members_key field in request.
		///</summary>
		[JsonPropertyName("members")]
		public WgnClansInfoMembers? Members { get; set; }

		///<summary>
		/// Private clan info
		///</summary>
		[JsonPropertyName("private")]
		public WgnClansInfoPrivate? Private { get; set; }
	}

    public class WgnClansInfoEmblems
    {
        ///<summary>
        /// 195x195 px Icons list
        ///</summary>
        [JsonPropertyName("x195")]
        public Dictionary<string, string>? X195 { get; set; }

		///<summary>
		/// 24x24 px Icons list
		///</summary>
		[JsonPropertyName("x24")]
        public Dictionary<string, string>? X24 { get; set; }

		///<summary>
		/// 256x256 px Icons list
		///</summary>
		[JsonPropertyName("x256")]
        public Dictionary<string, string>? X256 { get; set; }

		///<summary>
		///  32x32 px Icons list
		///</summary>
		[JsonPropertyName("x32")]
        public Dictionary<string, string>? X32 { get; set; }

		///<summary>
		/// 64x64 px Icons list
		///</summary>
		[JsonPropertyName("x64")]
        public Dictionary<string, string>? X64 { get; set; }
	}

    public class WgnClansInfoMembers
    {
        ///<summary>
        /// Clan member account id
        ///</summary>
        [JsonPropertyName("account_id")]
        public long? AccountId { get; set; }

        ///<summary>
        /// Clan member nick
        ///</summary>
        [JsonPropertyName("account_name")]
        public string? AccountName { get; set; }

        ///<summary>
        /// Date of joining the clan
        ///</summary>
        [JsonPropertyName("joined_at")]
        public int? JoinedAt { get; set; }

        ///<summary>
        /// Clan member role
        ///</summary>
        [JsonPropertyName("role")]
        public string? Role { get; set; }

        ///<summary>
        /// Localized member role
        ///</summary>
        [JsonPropertyName("role_i18n")]
        public string? RoleI18n { get; set; }
	}

    public class WgnClansInfoPrivate
    {
        [JsonPropertyName("online_members")]
        public int[]? OnlineMembers { get; set; }

        [JsonPropertyName("treasury")]
        public long? Treasury { get; set; }
	}
}