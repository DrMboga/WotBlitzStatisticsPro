namespace WotBlitzStatisticsPro.WebUi.Model
{
    /// <inheritdoc />
    public class WargamingApiSettings : IWargamingApiSettings
    {
        /// <inheritdoc />
        public string ApplicationId { get; set; } = string.Empty;
        /// <inheritdoc />
        public string BlitzApiUrl { get; set; } = string.Empty;
         /// <inheritdoc />
        public string WotApiUrl { get; set; } = string.Empty;
        /// <inheritdoc />
        public bool UseMockData { get; set; } = false;
    } 
}