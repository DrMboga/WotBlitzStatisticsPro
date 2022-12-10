namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public interface IWargamingClient
    {
        Task<T?> GetFromBlitzApi<T>(
			RequestLanguage language,
			string method,
			params string[] queryParameters) where T: class;

        Task<T?> CallWgApi<T>(string uri, bool postMethod = false) where T : class;
    }
}