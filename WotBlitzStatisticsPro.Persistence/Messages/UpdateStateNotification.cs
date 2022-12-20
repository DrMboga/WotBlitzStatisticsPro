namespace WotBlitzStatisticsPro.Persistence.Messages
{
    public record UpdateStateNotification(DateTime DictionariesUpdated, string Locale, string GameVersion): INotification;
}