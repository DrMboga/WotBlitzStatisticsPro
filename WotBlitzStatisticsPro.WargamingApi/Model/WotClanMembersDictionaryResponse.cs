namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotClanMembersDictionaryResponse
    {
        /// <summary>
        /// Clan roles list
        /// </summary>
        [JsonPropertyName("clans_roles")]
        public Dictionary<string, string>? ClanRoles { get; set; }

        /// <summary>
        /// Clan settings list
        /// </summary>
        [JsonPropertyName("settings")]
        public Dictionary<string, long>? Settings { get; set; }

    }
}