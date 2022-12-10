namespace WotBlitzStatisticsPro.Dto
{
    public class ShortPlayerInfoDto
    {
        public long AccountId { get; set;}

        public string Nickname { get; set;} = string.Empty;

        public DateTimeOffset CreatedAt { get; set;}

        public long Battles { get; set;}

        public DateTimeOffset LastBattle { get; set;}

        public string? ClanTag { get; set;}

        public int WinRate { get; set;}
    }
}