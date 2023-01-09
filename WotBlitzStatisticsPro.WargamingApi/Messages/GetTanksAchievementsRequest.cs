namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetTanksAchievementsRequest(long AccountId, long[]? TankIds, RequestLanguage Language): IRequest<WotAccountAchievementResponse[]>;
}