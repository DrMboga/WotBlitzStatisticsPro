using System.Text.Json.Serialization;

namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotAccountStatistics
    {
        ///<summary>
		/// Amount of destroyed by player tanks.
		///</summary>
		[JsonPropertyName("frags")]
		public Dictionary<string, string>? Frags { get; set; }

		///<summary>
		/// Full player's statistics
		///</summary>
		[JsonPropertyName("all")]
		public WotAccountFullStatistics? All { get; set; }

    }
}