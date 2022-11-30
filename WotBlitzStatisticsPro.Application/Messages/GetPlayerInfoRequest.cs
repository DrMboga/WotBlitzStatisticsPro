using MediatR;
using WotBlitzStatisticsPro.Application.Dto;

namespace WotBlitzStatisticsPro.Application.Messages
{
    public record GetPlayerInfoRequest(long accountId, string locale): IRequest<PlayerInfoDto>;
}