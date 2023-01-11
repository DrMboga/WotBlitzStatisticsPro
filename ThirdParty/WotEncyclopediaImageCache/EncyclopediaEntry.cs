using System.Text.Json.Serialization;
namespace WotEncyclopediaImageCache
{
    public class EncyclopediaEntry
    {
        [JsonPropertyName("images")]
		public Dictionary<string, string>? Images { get; set; }
    }
}