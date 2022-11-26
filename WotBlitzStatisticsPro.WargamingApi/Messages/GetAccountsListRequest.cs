using MediatR;
using WotBlitzStatisticsPro.WargamingApi.Model;

namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetAccountsListRequest(string SearchString): IRequest<List<WotAccountListResponse>>;
}