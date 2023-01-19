namespace WotBlitzStatisticsPro.Application.Messages
{
    public record SaveNewPlayerSessionNotification(PlayerInfoDto Player): INotification;

}