using System.Globalization;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Helpers;
using WotBlitzStatisticsPro.Application.Messages;
using WotBlitzStatisticsPro.WebUi.Model;

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

        public ResourcePlanTableRow[]? ResourcePlans { get; set; }

        public int? TotalCount { get; set; }

        public long? TotalPrice { get; set; }

        public long? TotalXp { get; set; }

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
                    await RefreshTable();
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
                // TODO: Refresh table
            }
        }

        public async Task RefreshTable()
        {
            if (Mediator != null && PlayerInfo != null)
            {
                var resourcePlans = await Mediator.Send(new GetResourcePlansRequest(PlayerInfo.AccountId));
                if (resourcePlans == null)
                {
                    return;
                }
                var vehicles = await Mediator.Send(new GetDictionaryVehiclesRequest());
                resourcePlans = resourcePlans
                    .OrderByDescending(p => p.Bought)
                    .ThenBy(p => p.Order)
                    .ToArray();

                var resourcePlansRows = new List<ResourcePlanTableRow>();

                foreach (var resourcePlan in resourcePlans)
                {
                    var vehicle = vehicles.FirstOrDefault(v => v.TankId == resourcePlan.TankId);
                    if(vehicle == null)
                    {
                        continue;
                    }
                    
                    int equipmentPrice = vehicle.Tier.CalculateEquipment(resourcePlan.PlanningEquipment);
                    long vehiclePriceWithDiscount = vehicle.PriceCredits == null 
                        ? 0
                        : Convert.ToInt64((decimal)vehicle.PriceCredits - ((decimal)vehicle.PriceCredits * resourcePlan.SaleCert));
                    
                    long modulesXpPrice = vehicle.Modules?.Where(m => m.IsDefault == false).Sum(m => m.PriceXp) ?? 0;

                    resourcePlansRows.Add(new ResourcePlanTableRow(
                        resourcePlan.Order,
                        resourcePlan.TankId,
                        vehicle.Nation,
                        vehicle.Tier,
                        vehicle.Type,
                        vehicle.Name,
                        vehicle.PreviewImage ?? string.Empty,
                        vehicle.PriceCredits ?? 0,
                        vehiclePriceWithDiscount + equipmentPrice,
                        modulesXpPrice,
                        resourcePlan.Bought
                    ));
                }

                ResourcePlans = resourcePlansRows.ToArray();
                TotalCount = ResourcePlans.Where(r => r.Bought == null).Count();
                TotalPrice = ResourcePlans.Where(r => r.Bought == null).Sum(r => r.TankPriceWithEquipmentAndDiscount);
                TotalXp = ResourcePlans.Where(r => r.Bought == null).Sum(r => r.TotalNonDefaultModulesPriceXp);
            }
        }

        public async Task MarkTankAsBought(long tankId)
        {


            await RefreshTable();
            StateHasChanged();
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

        public string Truncate(long amount)
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