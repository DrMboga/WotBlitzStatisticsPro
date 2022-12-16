namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotEncyclopediaAchievementsResponse
    {
		[JsonPropertyName("achievement_id")]
		public string? AchievementId { get; set; }

		///<summary>
		/// Achievement condition
		///</summary>
		[JsonPropertyName("condition")]
		public string? Condition { get; set; }

		///<summary>
		/// Achievement description
		///</summary>
		[JsonPropertyName("description")]
		public string? Description { get; set; }

		///<summary>
		/// Historical info
		///</summary>
		[JsonPropertyName("hero_info")]
		public string? HeroInfo { get; set; }

		///<summary>
		/// Image link
		///</summary>
		[JsonPropertyName("image")]
		public string? Image { get; set; }

		///<summary>
		/// Image 180x180px link
		///</summary>
		[JsonPropertyName("image_big")]
		public string? ImageBig { get; set; }

		///<summary>
		/// Achievement name
		///</summary>
		[JsonPropertyName("name")]
		public string? Name { get; set; }

		///<summary>
		/// Sort order
		///</summary>
		[JsonPropertyName("order")]
		public long? Order { get; set; }

		///<summary>
		/// Is outdated achievement
		///</summary>
		[JsonPropertyName("outdated")]
		public bool Outdated { get; set; }

		///<summary>
		/// Achievement section
		///</summary>
		[JsonPropertyName("section")]
		public string? Section { get; set; }

		///<summary>
		/// Achievement section order
		///</summary>
		[JsonPropertyName("section_order")]
		public long? SectionOrder { get; set; }

		///<summary>
		/// Achievement Type
		///</summary>
		[JsonPropertyName("type")]
		public string? Type { get; set; }

		///<summary>
		/// Achievement options
		///</summary>
		[JsonPropertyName("options")]
		public WotEncyclopediaAchievementsOptions[]? Options { get; set; }
	}

	public class WotEncyclopediaAchievementsOptions
	{

		///<summary>
		/// Achievement description
		///</summary>
		[JsonPropertyName("description")]
		public string? Description { get; set; }

		///<summary>
		/// Image link
		///</summary>
		[JsonPropertyName("image")]
		public string? Image { get; set; }

		///<summary>
		/// Image 180x180px link
		///</summary>
		[JsonPropertyName("image_big")]
		public string? ImageBig { get; set; }

		[JsonPropertyName("name")]
		public string? Name { get; set; }
	}
}