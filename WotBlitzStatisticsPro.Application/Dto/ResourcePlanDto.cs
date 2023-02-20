namespace WotBlitzStatisticsPro.Application.Dto
{
    public class ResourcePlanDto
    {
        public long TankId { get; set; }
        public int Order { get; set; }
        public decimal SaleCert { get; set; }
        public int PlanningEquipment { get; set; }
        public DateTime? Bought { get; set; }
    }
}