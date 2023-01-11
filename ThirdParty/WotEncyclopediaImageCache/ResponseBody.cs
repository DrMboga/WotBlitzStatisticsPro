using System.Text.Json.Serialization;
namespace WotEncyclopediaImageCache
{
	public class ResponseBody<T> where T: class
	{
		[JsonPropertyName("status")]
		public string? Status { get; set; }

		[JsonPropertyName("data")]
		public T? Data { get; set; }
	}
}