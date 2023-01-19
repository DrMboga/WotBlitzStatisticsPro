using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace WotBlitzStatisticsPro.WebUi.Pages
{
    public class PlannerBase: ComponentBase
    {
        [Inject]
        public IStringLocalizer<App>? Localize { get; set; }

        [Inject]
        public IMediator? Mediator { get; set; }

        public bool IsPlayerLoggedIn { get; set; } = false;
    }
}