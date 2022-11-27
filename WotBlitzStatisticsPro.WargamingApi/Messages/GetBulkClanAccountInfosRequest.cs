using MediatR;
using WotBlitzStatisticsPro.WargamingApi.Model;

namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetBulkClanAccountInfosRequest(long[] AccountIds): IRequest<List<ClanAccountInfo>>;
}