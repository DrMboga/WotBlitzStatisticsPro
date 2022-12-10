namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetBulkClanAccountInfosRequest(long[] AccountIds): IRequest<List<ClanAccountInfo>>;
}