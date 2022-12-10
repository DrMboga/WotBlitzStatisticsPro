namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotAccountPrivateInfoGroupedContacts
    {
        ///<summary>
		/// Blocked accountIds
		///</summary>
		[JsonPropertyName("blocked")]
		public int[]? Blocked { get; set; }

		///<summary>
		/// Groups
		///</summary>
		[JsonPropertyName("groups")]
		public Dictionary<string, string>? Groups { get; set; }

		///<summary>
		/// Ungrouped accountIds
		///</summary>
		[JsonPropertyName("ungrouped")]
		public int[]? Ungrouped { get; set; }

    }
}