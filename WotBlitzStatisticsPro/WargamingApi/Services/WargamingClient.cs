using System.Net.Http.Headers;
using System.Text;

namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public class WargamingClient: IWargamingClient
    {
        private readonly HttpClient _httpClient;
        private readonly IWargamingApiSettings _wargamingApiSettings;
		private readonly ICacheService _cacheService;

        public WargamingClient(
			HttpClient httpClient, 
			IWargamingApiSettings wargamingApiSettings, 
			ICacheService cacheService)
        {
            _httpClient = httpClient;
            _wargamingApiSettings = wargamingApiSettings;
            _cacheService = cacheService;
        }

        public Task<T?> GetFromBlitzApi<T>(
			RequestLanguage language,
			string method,
			params string[] queryParameters) where T: class
		{
			string uri = GetBlitzUri(language, method, queryParameters);
			return CallWgApi<T>(uri);
        }

		public async Task<T?> CallWgApi<T>(string uri, bool postMethod = false) where T : class
		{
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			HttpResponseMessage? response;
			if(postMethod)
            {
				// Create httpContent
				var requestBody = TransformUriToRequestBody(uri);
				var httpContent = new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

				response = await _httpClient.PostAsync(uri, httpContent);
            }
            else
            {
				string? cachedResponse = _cacheService.CachedRequest(uri);
				if (string.IsNullOrEmpty(cachedResponse))
				{
					response = await _httpClient.GetAsync(uri);
				}
				else
				{
					var cachedObject = JsonSerializer.Deserialize<ResponseBody<T>>(cachedResponse);
					return cachedObject?.Data;
				}
			}
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();

			var responseBody = JsonSerializer.Deserialize<ResponseBody<T>>(responseString);

			// _logger.LogInformation($"HTTP {(postMethod ? "POST": "GET")} {uri} - {responseBody.Status}");

            if(responseBody != null)
            {
                switch (responseBody.Status)
                {
                    case "ok":
						_cacheService.PutToCache(uri, responseString);
                        return responseBody.Data;
                    case "error":
                        {
                            var error = responseBody.Error;
                            var message = $"Field:{(error?.Field ?? "undefined")}  Message:{(error?.Message ?? "undefined")}  Value:{(error?.Value ?? "undefined")}  Code:{(error?.Code ?? "undefined")}";
                            throw new ArgumentException(message);
                        }
                    default:
                        throw new ArgumentException($"Unexpected response body status '{responseBody.Status}'");
                }
            }
            throw new ArgumentException($"Response body is empty");
		}

        protected string GetWotUri(string method, string[]? queryParameters)
		{
			var uri = new StringBuilder($"{_wargamingApiSettings.WotApiUrl}{method}");
			uri.Append("?application_id=")
				.Append(_wargamingApiSettings.ApplicationId);
			if (queryParameters != null)
			{
				foreach (var param in queryParameters)
				{
					uri.Append("&")
						.Append(param);
				}
			}
			return uri.ToString();
		}

		private string GetBlitzUri(RequestLanguage language, string method, string[] queryParameters)
		{
			var uri = new StringBuilder($"{_wargamingApiSettings.BlitzApiUrl}{method}");
			uri.Append("?application_id=")
				.Append(_wargamingApiSettings.ApplicationId)
				.Append("&language=")
				.Append(language.ToString().ToLower());
			if (queryParameters != null)
			{
				foreach (var param in queryParameters)
				{
					uri.Append("&")
						.Append(param);
				}
			}
			return uri.ToString();
		}

		private string TransformUriToRequestBody(string request)
		{
			return request.Substring(request.IndexOf('?') + 1);
		}
    }
}