namespace WotBlitzStatisticsPro.WargamingApi.Tests
{
    public class WargamingApiSettings : IWargamingApiSettings
    {
        public string ApplicationId { get; set;} = "Demo";
        public string BlitzApiUrl { get; set;} = "https://demo.wot.blitz.eu/";
        public string WotApiUrl { get; set;} = "https://demo.wot.eu/"; 
        public bool UseMockData { get; set;} = false;
    }
}