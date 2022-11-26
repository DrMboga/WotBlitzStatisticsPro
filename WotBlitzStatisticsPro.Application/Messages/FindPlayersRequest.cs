using MediatR;
using WotBlitzStatisticsPro.Application.Dto;

namespace WotBlitzStatisticsPro.Application.Messages
{
    public record FindPlayersRequest(string searchString): IRequest<List<PlayerInfoDto>>;
}