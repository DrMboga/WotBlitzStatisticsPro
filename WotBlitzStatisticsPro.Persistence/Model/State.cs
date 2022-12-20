namespace WotBlitzStatisticsPro.Persistence.Model
{
    public class State
    {
        public long Id { get; set; }
        public DateTime? DictionariesUpdated { get; set; }
        public string? GameVersion { get; set; }
        public string DictionariesLanguage { get; set; } = "En";

        public long? LoggedInAccountId { get; set; }
        public string? WgToken { get; set; }
        public DateTime? WgTokenExpiration { get; set; }
    }
}