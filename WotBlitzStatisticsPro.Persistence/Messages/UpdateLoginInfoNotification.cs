namespace WotBlitzStatisticsPro.Persistence.Messages
{
    public record UpdateLoginInfoNotification(long AccountId, string AuthToken, DateTime Expiration): INotification;
}