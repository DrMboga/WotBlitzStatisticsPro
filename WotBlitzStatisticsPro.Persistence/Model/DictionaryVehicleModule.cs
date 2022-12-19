namespace WotBlitzStatisticsPro.Persistence.Model
{
    public class DictionaryVehicleModule
    {
        public long ModuleId { get; set; }

        public int Tier { get; set; }

        public long Weight { get; set; }

        public string Nation { get; set; }= string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ModuleType { get; set; } = string.Empty;

        public List<DictionaryVehicleModuleRelation>? VehicleModulesRelation { get; set; }
    }
}