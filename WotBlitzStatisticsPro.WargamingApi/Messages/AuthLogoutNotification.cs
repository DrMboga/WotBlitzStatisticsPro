namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record AuthLogoutNotification(string AuthToken): INotification;
}