namespace WotBlitzStatisticsPro.WargamingApi.Tests
{
    public class WargamingClientTests
    {
        private List<WotAccountListResponse> _expectedResponse;

        private string _httpResponseContent;

        private Mock<HttpMessageHandler> _httpMessagesHandlerMock;
        private HttpClient _httpClient;

        private IWargamingApiSettings _settings;

        private Mock<ICacheService> _cacheServiceMock;

        private WargamingClient _service;

        [SetUp]
        protected void Setup()
        {
            _expectedResponse = new List<WotAccountListResponse> {
                new WotAccountListResponse { AccountId = 12345, Nickname = "First"},
                new WotAccountListResponse { AccountId = 1234567, Nickname = "Second"}
            };

            var responseBody = new ResponseBody<List<WotAccountListResponse>> {
                Data = _expectedResponse,
                Status = "ok"
            };

            _httpResponseContent = JsonSerializer.Serialize(responseBody);

            _settings = new WargamingApiSettings();
            _httpMessagesHandlerMock = new ();
            _httpClient = new HttpClient(_httpMessagesHandlerMock.Object);
            _cacheServiceMock = new ();

            _service = new WargamingClient(
                _httpClient,
                _settings,
                _cacheServiceMock.Object
            );
        }

        [Test]
        public async Task ShouldMakeAppropriateHttpRequestToWargamingApi()
        {
            string accountsListGetMethod = "account/list/";
            string searchStringParameter = "search=One2Three";
            string expectedUrl = $"{_settings.BlitzApiUrl}{accountsListGetMethod}?application_id={_settings.ApplicationId}&language=en&{searchStringParameter}";

            _httpMessagesHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", 
                    ItExpr.Is<HttpRequestMessage>(m => m.RequestUri!.ToString() == expectedUrl), 
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(_httpResponseContent)
                });

            var response = await _service.GetFromBlitzApi<List<WotAccountListResponse>>(
                WotBlitzStatisticsPro.Model.RequestLanguage.En, 
                accountsListGetMethod,
                searchStringParameter);

            response.Should().NotBeNull();
            response?.Count.Should().Be(_expectedResponse.Count);

            _httpMessagesHandlerMock
                .Protected()
                .Verify<Task<HttpResponseMessage>>("SendAsync", Times.Once(), 
                    ItExpr.Is<HttpRequestMessage>(m => m.RequestUri!.ToString() == expectedUrl),
                    ItExpr.IsAny<CancellationToken>());

            _cacheServiceMock
                .Verify(c => c.PutToCache(expectedUrl, _httpResponseContent), Times.Once());
        }

        [Test]
        public async Task ShouldNotCallApiIfResponseCached()
        {
            string accountsListGetMethod = "account/list/";
            string searchStringParameter = "search=One2Three";
            string expectedUrl = $"{_settings.BlitzApiUrl}{accountsListGetMethod}?application_id={_settings.ApplicationId}&language=en&{searchStringParameter}";

            _cacheServiceMock.Setup(c => c.CachedRequest(expectedUrl)).Returns(_httpResponseContent);

            var response = await _service.GetFromBlitzApi<List<WotAccountListResponse>>(
                WotBlitzStatisticsPro.Model.RequestLanguage.En, 
                accountsListGetMethod,
                searchStringParameter);

            response.Should().NotBeNull();
            response?.Count.Should().Be(_expectedResponse.Count);

            _httpMessagesHandlerMock
                .Protected()
                .Verify<Task<HttpResponseMessage>>("SendAsync", Times.Never(), 
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>());

            _cacheServiceMock
                .Verify(c => c.PutToCache(expectedUrl, _httpResponseContent), Times.Never());
        }
    }
}