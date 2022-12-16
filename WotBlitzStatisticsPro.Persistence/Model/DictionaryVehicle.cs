namespace WotBlitzStatisticsPro.Persistence.Model
{
    public class DictionaryVehicle
    {
        public long TankId { get; set; }
        public short Tier { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Nation { get; set; } = string.Empty;
        public bool IsPremium { get; set; }

        public long? PriceGold { get; set; }
        public long? PriceCredits { get; set; }

        // TODO: Create mapping one to many relation. The mapping by rows in the tree - read static json file from wwwroot
        public long? NextTankMappingId { get; set; }

        public string Image { get; set; } = string.Empty;
        public string PreviewImage { get; set; } = string.Empty;

        // TODO: These 2 props depends on request language in settings table. If user changes the language, this will be overridden
        public string? Name { get; set; }
        public string? Description { get; set; }

        // TODO: Create many-to-many relation with "encyclopedia/modules/" request.
        // Table should contain module Id, Tank Id, ModuleType, and XP
        // Then collect Turrets, Suspensions and Engines from WotEncyclopediaVehiclesResponse into this table for each tank ID
    }
}