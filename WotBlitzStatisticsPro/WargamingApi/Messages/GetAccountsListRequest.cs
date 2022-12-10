namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetAccountsListRequest(string SearchString): IRequest<List<WotAccountListResponse>>;
}