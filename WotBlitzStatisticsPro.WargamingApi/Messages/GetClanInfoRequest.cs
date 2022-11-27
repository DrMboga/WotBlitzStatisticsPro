using MediatR;
using WotBlitzStatisticsPro.Model;
using WotBlitzStatisticsPro.WargamingApi.Model;

namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetClanInfoRequest(long[] ClanIds, RequestLanguage Language = RequestLanguage.En): IRequest<List<ClanInfo>>;
}