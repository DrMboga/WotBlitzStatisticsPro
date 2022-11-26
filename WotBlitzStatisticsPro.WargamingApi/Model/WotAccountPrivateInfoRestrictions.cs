using System.Text.Json.Serialization;

namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotAccountPrivateInfoRestrictions
    {
        ///<summary>
		/// Clan chat ban time
		///</summary>
		[JsonPropertyName("chat_ban_time")]
		public int? ChatBanTime { get; set; }

    }
}