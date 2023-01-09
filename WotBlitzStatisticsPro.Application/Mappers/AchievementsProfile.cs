namespace WotBlitzStatisticsPro.Application.Mappers
{
    public static class AchievementsProfile
    {
        public static AchievementsDto ToAchievementsDto(this Dictionary<string, int>? achievements)
        {
            return new AchievementsDto(
                achievements.GetFromDictionary("markOfMasteryIII"),
                achievements.GetFromDictionary("markOfMasteryII"),
                achievements.GetFromDictionary("markOfMasteryI"),
                achievements.GetFromDictionary("markOfMastery"),
                achievements.GetFromDictionary("heroesOfRassenay"),
                achievements.GetFromDictionary("medalLafayettePool"),
                achievements.GetFromDictionary("medalRadleyWalters"),
                achievements.GetFromDictionary("medalKolobanov"),
                achievements.GetFromDictionary("warrior"),
                achievements.GetFromDictionary("mainGun")
            );
        }

        private static int GetFromDictionary(this Dictionary<string, int>? achievements, string key)
        {
            if(achievements == null)
            {
                return 0;
            }
            return achievements.ContainsKey(key) ? achievements[key] : 0;
        }
    }
}