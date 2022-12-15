namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotEncyclopediaInfoResponse
    {
		///<summary>
        /// Game client version
        ///</summary>
        [JsonPropertyName("game_version")]
        public string? GameVersion { get; set; }

        ///<summary>
        /// List of supported languages
        ///</summary>
        [JsonPropertyName("languages")]
        public Dictionary<string, string>? Languages { get; set; }

        ///<summary>
        /// Encyclopedia last update time
        ///</summary>
        [JsonPropertyName("tanks_updated_at")]
        public int? TanksUpdatedAt { get; set; }

        ///<summary>
        /// Cru clan role
        ///</summary>
        [JsonPropertyName("vehicle_crew_roles")]
        public Dictionary<string, string>? VehicleCrewRoles { get; set; }

        ///<summary>
        /// Vehicle nations dictionary
        ///</summary>
        [JsonPropertyName("vehicle_nations")]
        public Dictionary<string, string>? VehicleNations { get; set; }

        ///<summary>
        /// Vehicle types dictionary
        ///</summary>
        [JsonPropertyName("vehicle_types")]
        public Dictionary<string, string>? VehicleTypes { get; set; }

        ///<summary>
        /// Achievements sections
        ///</summary>
        [JsonPropertyName("achievement_sections")]
        public Dictionary<string, WotEncyclopediaInfoAchievementSection>? AchievementSections { get; set; }

	}

    public class WotEncyclopediaInfoAchievementSection
    {

        ///<summary>
        /// Achievements section name
        ///</summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        ///<summary>
        /// Achievement section order
        ///</summary>
        [JsonPropertyName("order")]
        public long? Order { get; set; }

    }
}