namespace WotBlitzStatisticsPro.Application.Messages
{
    public record AddResourcePlanningNotification(long TankId, decimal SaleCert, int PlanningEquipment): INotification;
}