using System.Text.Json.Serialization;

namespace WotBlitzStatisticsPro.WargamingApi.Model
{
	public class ResponseBody<T> where T: class
	{
		[JsonPropertyName("status")]
		public string? Status { get; set; }

		[JsonPropertyName("meta")]
		public Meta? Meta { get; set; }

		[JsonPropertyName("data")]
		public T? Data { get; set; }

		[JsonPropertyName("error")]
		public Error? Error { get; set; }
	}

	public class Meta
	{
		[JsonPropertyName("count")]
		public int Count { get; set; }
	}

	public class Error
	{
		[JsonPropertyName("field")]
		public string? Field { get; set; }

		[JsonPropertyName("message")]
		public string? Message { get; set; }

		[JsonPropertyName("code")]
		public string? Code { get; set; }

		[JsonPropertyName("value")]
		public string? Value { get; set; }
	}

}