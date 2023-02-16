using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using WotBlitzStatisticsPro.Application.Messages;

namespace WotBlitzStatisticsPro.WebUi.Pages
{
    public class PlannerBase: ComponentBase
    {
        [Inject]
        public IStringLocalizer<App>? Localize { get; set; }

        [Inject]
        public IMediator? Mediator { get; set; }

        public bool IsPlayerLoggedIn { get; set; } = false;

        public long? PlayerId { get; set; }
        public string? PlayerNick { get; set; }

        protected async override Task OnInitializedAsync()
        {
            if(Mediator != null)
            {
                PlayerId = await Mediator.Send(new GetLoggedInPlayerNameRequest());
                IsPlayerLoggedIn = PlayerId.HasValue;
                // TODO: load player nick, tanks and resources
            }
        }

        public async Task Logout()
        {
            if(Mediator != null)
            {
                await Mediator.Publish(new LogoutPlayerNotification());
                IsPlayerLoggedIn = false;
                PlayerId = null;
                PlayerNick = null;
            }
        }
    }
}