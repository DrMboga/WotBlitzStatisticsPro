namespace WotBlitzStatisticsPro.Persistence.Model
{
    public class DictionaryVehicleModuleRelation
    {
        public long ModuleId { get; set; }
        public DictionaryVehicleModule? Module { get; set; }

        public long TankId { get; set; }
        public DictionaryVehicle? Tank { get; set; }
    }
}