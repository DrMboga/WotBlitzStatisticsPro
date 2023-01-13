using WotBlitzStatisticsPro.Model;

namespace WotBlitzStatisticsPro.WebUi.Model
{
    public class TankTreeItem
    {
        public long TankId { get; set; }

        public int Tier { get; set; }

        public int Row { get; set; }

        public string TankTypeId { get; set; } = string.Empty;

        public bool IsPremium { get; set; }

        public bool IsResearched { get; set; }

        public string PreviewImage { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public MarkOfMastery? MarkOfMastery { get; set; }

        public decimal? WinRate { get; set; }

        public decimal? AvgDamage { get; set; }

        public long? Battles { get; set; }

        public DateTimeOffset? LastBattleTime { get; set; }

        public long? PriceCredit { get; set; }

        public List<TankTreeRowMap>? NextRows { get; set; }
    }
}