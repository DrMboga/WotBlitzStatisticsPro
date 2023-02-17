namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetAccountPrivateInfoRequest(long AccountId, RequestLanguage Language, string AccessToken): IRequest<WotAccountInfo>;
}