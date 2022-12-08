using MediatR;
using WotBlitzStatisticsPro.Application.Dto;

namespace WotBlitzStatisticsPro.WebUi.Messages
{
    public record GetPlayerInfoRequest(long accountId, string locale): IRequest<PlayerInfoDto>;
}