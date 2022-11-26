using WotBlitzStatisticsPro.Model;

namespace WotBlitzStatisticsPro.Application.Dto
{
    public class PlayerInfoDto : IPlayerInfo
    {
        public long AccountId { get; set;}

        public string Nickname { get; set;} = string.Empty;

        public DateTimeOffset CreatedAt { get; set;}

        public long Battles { get; set;}

        public DateTimeOffset LastBattle { get; set;}

        public long? MaxFragsTankId { get; set;}

        public long? MaxXpTankId { get; set;}

        public string? ClanTag { get; set;}

        public int WinRate { get; set;}

        public double? AvgTier { get; set;}
    }

}