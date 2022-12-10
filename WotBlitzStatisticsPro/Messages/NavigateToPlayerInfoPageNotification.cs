namespace WotBlitzStatisticsPro.Messages
{
    /// <summary>
    /// Command to save "IsDarkTheme" parameter to the local storage
    /// </summary>
    public record NavigateToPlayerInfoPageNotification(long accountId): INotification;
}