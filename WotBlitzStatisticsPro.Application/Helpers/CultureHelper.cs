using WotBlitzStatisticsPro.Model;

namespace WotBlitzStatisticsPro.Application.Helpers
{
    public static class CultureHelper
    {
        public static RequestLanguage ConvertCulture(this string culture)
        {
            switch(culture)
            {
                case "ru-RU":
                    return RequestLanguage.Ru;
                case "de-DE":
                    return RequestLanguage.De;
                default:
                    return RequestLanguage.En;
            }
        }
    }
}