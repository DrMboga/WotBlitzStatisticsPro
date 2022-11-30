using MediatR;
using Microsoft.AspNetCore.Components;
using WotBlitzStatisticsPro.WebUi.Messages;

namespace WotBlitzStatisticsPro.WebUi.Services
{
    public class NavigationService : INotificationHandler<NavigateToPlayerInfoPageNotification>
    {
        private readonly NavigationManager _navigationManager;

        public NavigationService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public Task Handle(NavigateToPlayerInfoPageNotification notification, CancellationToken cancellationToken)
        {
            _navigationManager.NavigateTo($"player/{notification.accountId}");
            return Task.CompletedTask;
        }
    }
}