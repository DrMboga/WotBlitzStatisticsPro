using Microsoft.AspNetCore.Components;
using WotBlitzStatisticsPro.Application.Dto;

namespace WotBlitzStatisticsPro.WebUi.Pages
{
    public class TanksTreeBase: ComponentBase
    {
        [Parameter]
        public TankInfoDto[]? TanksList { get; set; }
    }
}