namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public class AuthenticationService :
        IRequestHandler<AuthProlongTokenRequest, WotAuthProlongateResponse>,
        INotificationHandler<AuthLogoutNotification>
    {
        private readonly IWargamingClient _wargamingClient;
        private IWargamingApiSettings _apiSettings;

        public AuthenticationService(IWargamingClient wargamingClient, IWargamingApiSettings apiSettings)
        {
            _wargamingClient = wargamingClient;
            _apiSettings = apiSettings;
        }

        public async Task Handle(AuthLogoutNotification notification, CancellationToken cancellationToken)
        {
            string prolongUri = $"{_apiSettings.WotApiUrl}auth/logout/?application_id={_apiSettings.ApplicationId}&access_token={notification.AuthToken}";
            await _wargamingClient.CallWgApi<WotAuthProlongateResponse>(prolongUri, true);
        }

        public async Task<WotAuthProlongateResponse> Handle(AuthProlongTokenRequest request, CancellationToken cancellationToken)
        {
            string prolongUri = $"{_apiSettings.WotApiUrl}auth/prolongate/?application_id={_apiSettings.ApplicationId}&access_token={request.AuthToken}";
            var prolongResult = await _wargamingClient.CallWgApi<WotAuthProlongateResponse>(prolongUri, true);
            return prolongResult ?? new WotAuthProlongateResponse();
        }
    }
}