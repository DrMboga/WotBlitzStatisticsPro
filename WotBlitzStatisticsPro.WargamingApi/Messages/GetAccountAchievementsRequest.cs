namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetAccountAchievementsRequest(long AccountId, RequestLanguage Language): IRequest<WotAccountAchievementResponse>;
}