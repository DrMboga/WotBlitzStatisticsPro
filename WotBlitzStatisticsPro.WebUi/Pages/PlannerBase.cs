using System.Globalization;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using WotBlitzStatisticsPro.Application.Dto;
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

        public PlayerPrivateInfoDto? PlayerInfo { get; set; }

        public TanksPlannerDialog? TanksPlannerDialog { get; set; }

        public ResourcePlanDto[]? ResourcePlans { get; set; }

        protected async override Task OnInitializedAsync()
        {
            if(Mediator != null)
            {
                var playerState = await Mediator.Send(new GetLoggedInPlayerRequest());
                IsPlayerLoggedIn = playerState?.LoggedInAccountId != null;
                if(IsPlayerLoggedIn && playerState != null && playerState.LoggedInAccountId.HasValue && !string.IsNullOrEmpty(playerState.WgToken))
                {
                    PlayerInfo = await Mediator.Send(new GetPlayerPrivateInfoRequest(
                        playerState.LoggedInAccountId.Value, 
                        CultureInfo.CurrentCulture.Name,
                        playerState.WgToken));
                    ResourcePlans = await Mediator.Send(new GetResourcePlansRequest(playerState.LoggedInAccountId.Value));
                }
            }
        }

        public async Task Logout()
        {
            if(Mediator != null)
            {
                await Mediator.Publish(new LogoutPlayerNotification());
                IsPlayerLoggedIn = false;
                PlayerInfo = null;
            }
        }

        public async Task AddNewTank()
        {
            if(TanksPlannerDialog != null && PlayerInfo?.Tanks != null) 
            {
                await TanksPlannerDialog.Open(PlayerInfo.Tanks);
                if (Mediator != null && PlayerInfo != null)
                {
                    ResourcePlans = await Mediator.Send(new GetResourcePlansRequest(PlayerInfo.AccountId));
                    StateHasChanged();
                }
            }
        }

        public string? Credits()
        {
            if (PlayerInfo == null)
            {
                return null;
            }
            return Truncate(PlayerInfo.Credits);
        }

        public string? FreeXp()
        {
            if (PlayerInfo == null)
            {
                return null;
            }
            return Truncate(PlayerInfo.FreeXp);
        }

        private string Truncate(long amount)
        {
            if (amount < 1000)
            {
                return amount.ToString("N0");
            }
            if (amount < 1000000)
            {
                return $"{(((decimal) amount / 1000m).ToString("N1"))} K";
            }
            return $"{(((decimal) amount / 1000000m).ToString("N1"))} M";
        }
    }
}