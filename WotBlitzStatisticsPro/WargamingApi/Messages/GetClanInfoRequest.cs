namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetClanInfoRequest(long[] ClanIds, RequestLanguage Language = RequestLanguage.En): IRequest<List<ClanInfo>>;
}