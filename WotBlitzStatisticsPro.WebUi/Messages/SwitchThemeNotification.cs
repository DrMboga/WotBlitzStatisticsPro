using MediatR;

namespace WotBlitzStatisticsPro.WebUi.Messages
{
    /// <summary>
    /// Command to switch css theme
    /// </summary>
    public record SwitchThemeNotification(bool IsDarkTheme) : INotification;
}