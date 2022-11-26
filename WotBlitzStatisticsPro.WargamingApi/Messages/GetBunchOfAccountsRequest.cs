using MediatR;
using WotBlitzStatisticsPro.WargamingApi.Model;

namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetBunchOfAccountsRequest(long[] AccountIds): IRequest<List<WotAccountInfo>>;
}