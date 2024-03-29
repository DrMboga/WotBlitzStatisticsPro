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

        /*
"next_tanks": {
"2561": 29000,
"16641": 31000
},
https://www.entityframeworktutorial.net/efcore/configure-one-to-many-relationship-using-fluent-api-in-ef-core.aspx

modelBuilder.Entity<DictionaryNextVehicle>()
            .HasOne<DictionaryVehicle>(s => s.Tank)
            .WithMany(g => g.NextVehicles)
            .HasForeignKey(s => s.TankId);
        */
        public List<DictionaryNextVehicle>? NextVehicles { get; set; }

        public int CurrentTankTreeRow { get; set; }

        public string? Image { get; set; } = string.Empty;
        public string? PreviewImage { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        // Many-to-many relation with modules.
        // https://www.entityframeworktutorial.net/efcore/configure-many-to-many-relationship-in-ef-core.aspx
        public List<DictionaryVehicleModuleRelation>? VehicleModulesRelation { get; set; }
    }
}