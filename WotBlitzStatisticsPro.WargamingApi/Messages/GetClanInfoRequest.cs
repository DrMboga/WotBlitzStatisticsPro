namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetClanInfoRequest(long ClanId, RequestLanguage Language = RequestLanguage.En): IRequest<ClanInfo>;
}