using MediatR;

namespace WotBlitzStatisticsPro.WebUi.Messages
{
    /// <summary>
    /// Command to save "IsDarkTheme" parameter to the local storage
    /// </summary>
    public record SaveThemeNotification(bool IsDarkTheme): INotification;
}