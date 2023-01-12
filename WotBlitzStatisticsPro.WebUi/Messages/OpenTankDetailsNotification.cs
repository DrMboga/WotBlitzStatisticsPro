using MediatR;
using WotBlitzStatisticsPro.Application.Dto;

namespace WotBlitzStatisticsPro.WebUi.Messages
{
    public record OpenTankDetailsNotification(TankInfoDto Tank): INotification;
}