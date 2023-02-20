namespace WotBlitzStatisticsPro.Persistence.Messages
{
    public record AddNewPlanningNotification(
        long AccountId,
        long TankId,
        int Order,
        decimal SaleCert,
        int PlanningEquipment
    ): INotification;

}