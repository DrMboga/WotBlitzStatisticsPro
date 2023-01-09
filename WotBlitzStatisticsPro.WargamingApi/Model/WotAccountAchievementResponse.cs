namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotAccountAchievementResponse
    {
        [JsonPropertyName("tank_id")]
        public long? TankId { get; set; }

		///<summary>
        /// Achievements
        ///</summary>
        [JsonPropertyName("achievements")]
        public Dictionary<string, int>? Achievements { get; set; }

        ///<summary>
        /// Max values if series achievements
        ///</summary>
        [JsonPropertyName("max_series")]
        public Dictionary<string, int>? MaxSeries { get; set; }
        
    }
}