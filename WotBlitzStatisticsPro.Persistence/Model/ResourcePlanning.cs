namespace WotBlitzStatisticsPro.Persistence.Model
{
    public class ResourcePlanning
    {
        public long PlanId { get; set; }
        public long AccountId { get; set; }
        public long TankId { get; set; }
        public int Order { get; set; }
        public decimal SaleCert { get; set; }
        public int PlanningEquipment { get; set; }
        public DateTime? Bought { get; set; }
    }
}