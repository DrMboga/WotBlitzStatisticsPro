using MediatR;
using WotBlitzStatisticsPro.Application.Dto;

namespace WotBlitzStatisticsPro.WebUi.Messages
{
    public record FindPlayersRequest(string SearchString): IRequest<List<ShortPlayerInfoDto>>;
}