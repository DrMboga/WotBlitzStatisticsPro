using System.Text.Json.Serialization;

namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotAccountFullStatistics
    {
        ///<summary>
		/// Battles count
		///</summary>
		[JsonPropertyName("battles")]
		public long Battles { get; set; }

		///<summary>
		/// Capture points
		///</summary>
		[JsonPropertyName("capture_points")]
		public long CapturePoints { get; set; }

		///<summary>
		/// Total damage amount
		///</summary>
		[JsonPropertyName("damage_dealt")]
		public long DamageDealt { get; set; }

		///<summary>
		/// Total amount of received damage
		///</summary>
		[JsonPropertyName("damage_received")]
		public long DamageReceived { get; set; }

		///<summary>
		/// Dropped capture points
		///</summary>
		[JsonPropertyName("dropped_capture_points")]
		public long DroppedCapturePoints { get; set; }

		///<summary>
		/// Total amount of frags
		///</summary>
		[JsonPropertyName("frags")]
		public long Frags { get; set; }

		///<summary>
		/// Total amount of fras grater ten 8 lvl
		///</summary>
		[JsonPropertyName("frags8p")]
		public long Frags8P { get; set; }

		///<summary>
		/// Total amount of hits
		///</summary>
		[JsonPropertyName("hits")]
		public long Hits { get; set; }

		///<summary>
		/// Total amount of losses
		///</summary>
		[JsonPropertyName("losses")]
		public long Losses { get; set; }

		///<summary>
		/// Max frags per battle
		///</summary>
		[JsonPropertyName("max_frags")]
		public long MaxFrags { get; set; }

		///<summary>
		/// Tank id, which kills max frags per battle
		///</summary>
		[JsonPropertyName("max_frags_tank_id")]
		public long MaxFragsTankId { get; set; }

		///<summary>
		/// Max experience per battle
		///</summary>
		[JsonPropertyName("max_xp")]
		public long MaxXp { get; set; }

		///<summary>
		/// Tank Id which created max experiense per battle
		///</summary>
		[JsonPropertyName("max_xp_tank_id")]
		public long MaxXpTankId { get; set; }

		///<summary>
		/// Total shots count
		///</summary>
		[JsonPropertyName("shots")]
		public long Shots { get; set; }

		///<summary>
		/// Total count of spotted vehicles
		///</summary>
		[JsonPropertyName("spotted")]
		public long Spotted { get; set; }

		///<summary>
		/// Total count of survived battles
		///</summary>
		[JsonPropertyName("survived_battles")]
		public long SurvivedBattles { get; set; }

		///<summary>
		/// Total count of suvived and winned battles
		///</summary>
		[JsonPropertyName("win_and_survived")]
		public long WinAndSurvived { get; set; }

		///<summary>
		/// Total wins count
		///</summary>
		[JsonPropertyName("wins")]
		public long Wins { get; set; }

		///<summary>
		/// Total amount of experience
		///</summary>
		[JsonPropertyName("xp")]
		public long Xp { get; set; }

    }
}