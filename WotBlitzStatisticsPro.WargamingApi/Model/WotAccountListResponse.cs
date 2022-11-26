using System.Text.Json.Serialization;

namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotAccountListResponse
    {
        ///<summary>
        /// Player accountId
        ///</summary>
        [JsonPropertyName("account_id")]
        public long? AccountId { get; set; }

        ///<summary>
        /// Player nick
        ///</summary>
        [JsonPropertyName("nickname")]
        public string? Nickname { get; set; }
    }
}