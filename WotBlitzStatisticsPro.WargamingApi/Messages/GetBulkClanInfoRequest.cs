namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetBulkClanInfoRequest(long[] ClanIds, RequestLanguage Language = RequestLanguage.En): IRequest<List<ClanInfo>>;
}