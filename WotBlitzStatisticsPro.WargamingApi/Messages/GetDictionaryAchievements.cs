namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetDictionaryAchievements(RequestLanguage Language): IRequest<List<WotEncyclopediaAchievementsResponse>>;
}