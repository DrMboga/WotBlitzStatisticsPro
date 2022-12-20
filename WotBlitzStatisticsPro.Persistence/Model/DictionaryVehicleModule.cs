namespace WotBlitzStatisticsPro.Persistence.Model
{
    public class DictionaryVehicleModule
    {
        public long TankId { get; set; }
        public DictionaryVehicle? Tank { get; set; }

        public long ModuleId { get; set; }

        public bool IsDefault { get; set; }

        public string Name { get; set; } = string.Empty;

        public long PriceCredit { get; set; }

		public long PriceXp { get; set; }

        public string Type { get; set; } = string.Empty;

    }
}