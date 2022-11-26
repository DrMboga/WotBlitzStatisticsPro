namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public class WargamingClient: IWargamingClient
    {
        private readonly HttpClient _httpClient;
        private readonly IWargamingApiSettings _wargamingApiSettings;

        public WargamingClient(HttpClient httpClient, IWargamingApiSettings wargamingApiSettings)
        {
            _httpClient = httpClient;
            _wargamingApiSettings = wargamingApiSettings;
        }
    }
}