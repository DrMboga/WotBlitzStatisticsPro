using WotBlitzStatisticsPro.WebUi.Model;

namespace WotBlitzStatisticsPro.WebUi.Services
{
    public interface ILocalStateService
    {
        Task<LocalState> ReadLocalState();

        Task<LocalState> SetTheme(bool isDarkTheme);

        Task<LocalState> SetLocale(string locale);
    }
}