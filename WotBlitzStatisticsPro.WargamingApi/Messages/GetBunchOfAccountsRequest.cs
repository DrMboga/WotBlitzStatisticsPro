namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetBunchOfAccountsRequest(long[] AccountIds): IRequest<List<WotAccountInfo>>;
}