using Microsoft.Extensions.Localization;
using WotBlitzStatisticsPro.WebUi.Model;

namespace WotBlitzStatisticsPro.WebUi.Helpers
{
    public static class DateDifferenceHelper
    {
        public static DateTime ConvertToLocalTime(this DateTimeOffset dateToConvert)
        {
            var specifiedTimeConvert = DateTime.SpecifyKind(dateToConvert.DateTime, DateTimeKind.Utc);
            return specifiedTimeConvert.ToLocalTime();
        }

        public static (int value, TimeInterval interval) ParseTheDifference(TimeSpan difference)
        {
            if (difference.TotalHours < 1)
            {
                return (Convert.ToInt32(difference.TotalMinutes), TimeInterval.Minute);
            }

            if (difference.TotalDays < 1)
            {
                return (Convert.ToInt32(difference.TotalHours), TimeInterval.Hour);
            }

            return difference.TotalDays switch
            {
                >= 365 => (CalculateYears(difference.TotalDays), TimeInterval.Year),
                (>= 30) and (< 365) => (CalculateMonths(difference.TotalDays), TimeInterval.Month),
                _ => (Convert.ToInt32(difference.TotalDays), TimeInterval.Day)
            };
        }

        private static int CalculateYears(double days)
        {
            return Convert.ToInt32(days) / 365;
        }

        private static int CalculateMonths(double days)
        {
            return Convert.ToInt32(days) / 30;
        }

    }
}