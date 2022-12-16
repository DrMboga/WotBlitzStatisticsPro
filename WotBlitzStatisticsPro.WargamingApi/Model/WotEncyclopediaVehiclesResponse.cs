namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotEncyclopediaVehiclesResponse
    {
		///<summary>
		/// Vehicle description
		///</summary>
		[JsonPropertyName("description")]
		public string? Description { get; set; }

		///<summary>
		/// Engines id list
		///</summary>
		[JsonPropertyName("engines")]
		public int[]? Engines { get; set; }

		///<summary>
		/// Guns id list
		///</summary>
		[JsonPropertyName("guns")]
		public int[]? Guns { get; set; }

		///<summary>
		/// Is vehicle premium
		///</summary>
		[JsonPropertyName("is_premium")]
		public bool IsPremium { get; set; }

		///<summary>
		/// Vehicle name
		///</summary>
		[JsonPropertyName("name")]
		public string? Name { get; set; }

		///<summary>
		/// Vehicle nationId
		///</summary>
		[JsonPropertyName("nation")]
		public string? Nation { get; set; }

		///<summary>
		/// List of next vehicles in tree:
		///
		/// key - vehicle id,
		/// value - amount of XP to open it
		///</summary>
		[JsonPropertyName("next_tanks")]
		public Dictionary<string, long>? NextTanks { get; set; }

		///<summary>
		/// Vehicle cost by pairs
		/// with keys: price_credit, price_gold
		///</summary>
		[JsonPropertyName("cost")]
		public Dictionary<string, long>? Cost { get; set; }

        ///<summary>
		/// Price in XP for parent vehicle as key
		///</summary>
		[JsonPropertyName("prices_xp")]
		public Dictionary<string, long>? PricesXp { get; set; }

		///<summary>
		/// List of compatible suspensions
		///</summary>
		[JsonPropertyName("suspensions")]
		public int[]? Suspensions { get; set; }

		///<summary>
		/// VehicleId
		///</summary>
		[JsonPropertyName("tank_id")]
		public long? TankId { get; set; }

		///<summary>
		/// Vehicle tier
		///</summary>
		[JsonPropertyName("tier")]
		public long? Tier { get; set; }

		///<summary>
		/// List of compatible turrets ids
		///</summary>
		[JsonPropertyName("turrets")]
		public int[]? Turrets { get; set; }

		///<summary>
		/// Vehicle type id
		///</summary>
		[JsonPropertyName("type")]
		public string? Type { get; set; }

		/////<summary>
		///// Default vehicle profile
		/////</summary>
		//[JsonPropertyName("default_profile")]
		//public WotEncyclopediaVehiclesDefaultProfile DefaultProfile { get; set; }

		///<summary>
		/// List of vehicle images.
		/// Keys: preview, normal
		///</summary>
		[JsonPropertyName("images")]
		public Dictionary<string, string>? Images { get; set; }

		/////<summary>
		///// Information about investigated modules
		/////</summary>
		//[JsonPropertyName("modules_tree")]
		//public WotEncyclopediaVehiclesModulesTree ModulesTree { get; set; }
	}
}