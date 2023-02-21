namespace WotBlitzStatisticsPro.Persistence.Messages
{
    public record UpdatePlanBoughtMarkNotification(long AccountId, long TankId, DateTime BuyDate): INotification;

}