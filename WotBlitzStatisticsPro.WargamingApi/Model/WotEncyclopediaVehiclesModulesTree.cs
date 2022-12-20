namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotEncyclopediaVehiclesModulesTree
    {
        ///<summary>
		/// Is default module
		///</summary>
		[JsonPropertyName("is_default")]
		public bool IsDefault { get; set; }

		///<summary>
		/// ModuleId
		///</summary>
		[JsonPropertyName("module_id")]
		public long? ModuleId { get; set; }

		///<summary>
		/// Module Name
		///</summary>
		[JsonPropertyName("name")]
		public string? Name { get; set; }

		///<summary>
		/// List of module identifies available after this module investigation
		///</summary>
		[JsonPropertyName("next_modules")]
		public int[]? NextModules { get; set; }

		///<summary>
		/// List of vehicle identifies available after this module investigation
		///</summary>
		[JsonPropertyName("next_tanks")]
		public int[]? NextTanks { get; set; }

		///<summary>
		/// Price in credit
		///</summary>
		[JsonPropertyName("price_credit")]
		public long? PriceCredit { get; set; }

		///<summary>
		/// Price of investigation
		///</summary>
		[JsonPropertyName("price_xp")]
		public long? PriceXp { get; set; }

		///<summary>
		/// Module type
		///</summary>
		[JsonPropertyName("type")]
		public string? Type { get; set; }
    }
}