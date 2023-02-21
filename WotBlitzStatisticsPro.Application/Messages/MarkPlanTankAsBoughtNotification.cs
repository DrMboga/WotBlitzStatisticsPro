namespace WotBlitzStatisticsPro.Application.Messages
{
    public record MarkPlanTankAsBoughtNotification(long AccountId, long TankId): INotification;
}