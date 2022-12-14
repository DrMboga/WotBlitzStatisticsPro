namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetClanAccountInfoRequest(long AccountId, RequestLanguage Language): IRequest<ClanAccountInfo?>;
}