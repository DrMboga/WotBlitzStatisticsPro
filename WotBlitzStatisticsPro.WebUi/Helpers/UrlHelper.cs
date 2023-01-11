namespace WotBlitzStatisticsPro.WebUi.Helpers
{
    public static class UrlHelper {
        public static string ToLocalVehiclesUrl(this string? fullUrl)
        {
            if(string.IsNullOrEmpty(fullUrl))
            {
                return string.Empty;
            }
            var url = new Uri(fullUrl);
            if(url.Segments == null || url.Segments.Length == 0)
            {
                return fullUrl;
            }
            var fileName = url.Segments[url.Segments.Length - 1];
            return $"vehicles/{fileName}";
        }
    }
}