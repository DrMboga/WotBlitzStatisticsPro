using MediatR;
using WotBlitzStatisticsPro.Application.Dto;

namespace WotBlitzStatisticsPro.Application.Messages
{
    public record FindPlayersRequest(string SearchString): IRequest<List<ShortPlayerInfoDto>>;
}