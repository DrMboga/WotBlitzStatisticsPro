using Microsoft.AspNetCore.Components;

namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerAuthenticationService : 
        IRequestHandler<GetLoggedInPlayerNameRequest, long?>,
        INotificationHandler<RedirectToLoginPlayerNotification>,
        INotificationHandler<RedirectFromWgLoginPlayerNotification>
    {
        private const int UpdateTokenBeforeExpirationInDays = -3;
        private readonly IMediator _mediator;
        private IWargamingApiSettings _apiSettings;
        private readonly NavigationManager _navigationManager;

        public PlayerAuthenticationService(
            IMediator mediator, 
            IWargamingApiSettings apiSettings, 
            NavigationManager navigationManager)
        {
            _mediator = mediator;
            _apiSettings = apiSettings;
            _navigationManager = navigationManager;
        }

        public async Task<long?> Handle(GetLoggedInPlayerNameRequest request, CancellationToken cancellationToken)
        {
            var state = await _mediator.Send(new ReadStateRequest());
            var today = DateTime.Now.Date;
            if (state == null || state.LoggedInAccountId == null || (state.WgTokenExpiration.HasValue && state.WgTokenExpiration < today))
            {
                return null;
            }

            if (state.WgTokenExpiration.HasValue && state.WgTokenExpiration.Value.AddDays(UpdateTokenBeforeExpirationInDays) < today)
            {
                // TODO: Send Prolong Token Notification
            }

            return state.LoggedInAccountId;
        }

        public Task Handle(RedirectToLoginPlayerNotification notification, CancellationToken cancellationToken)
        {
            var baseUri = _navigationManager.BaseUri;
            var redirectUrl = $"{_apiSettings.WotApiUrl}auth/login/?application_id={_apiSettings.ApplicationId}&redirect_uri={baseUri}login";
            _navigationManager.NavigateTo(redirectUrl);
            return Task.CompletedTask;
        }

        public async Task Handle(RedirectFromWgLoginPlayerNotification notification, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new UpdateLoginInfoNotification(
                notification.AccountId, 
                notification.AccessToken, 
                Convert.ToInt32(notification.ExpiresAt).ToDateTime()));
            _navigationManager.NavigateTo("/planner");
        }
    }
}