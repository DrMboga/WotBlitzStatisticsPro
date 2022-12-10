namespace WotBlitzStatisticsPro.Messages
{
    /// <summary>
    /// Command to switch css theme
    /// </summary>
    public record SwitchThemeNotification(bool IsDarkTheme) : INotification;
}