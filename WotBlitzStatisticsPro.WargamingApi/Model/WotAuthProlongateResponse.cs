namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotAuthProlongateResponse
    {
        [JsonPropertyName("access_token")]
		public string AccessToken { get; set; } = string.Empty;

		[JsonPropertyName("account_id")]
		public long AccountId { get; set; }

		[JsonPropertyName("expires_at")]
		public int ExpirationTimeStamp { get; set; }
    }
}