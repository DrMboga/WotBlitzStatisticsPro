namespace WotBlitzStatisticsPro.Application.Messages
{
    public record RedirectFromWgLoginPlayerNotification(
        string Nickname,
        long AccountId,
        string AccessToken,
        long ExpiresAt
    ): INotification;
}