namespace WotBlitzStatisticsPro.Persistence.Model
{
    public class DictionaryNextVehicle
    {
        public int Id { get; set; }

        public long TankId { get; set; }

        public DictionaryVehicle? Tank { get; set; }

        public long NextTankId { get; set; }

        public long PriceXP { get; set; }

        public int TreeRowIndex { get; set; }
    }
}