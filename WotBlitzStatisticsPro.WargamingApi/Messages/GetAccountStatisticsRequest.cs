namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetAccountStatisticsRequest(long AccountId, RequestLanguage Language): IRequest<WotAccountInfo>;
}