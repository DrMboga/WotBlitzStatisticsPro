namespace WotBlitzStatisticsPro.Application.Mappers
{
    public static class PlayerSessionProfile
    {
        public static PlayerSession ToPlayerSession(this PlayerInfoDto player)
        {
            return new PlayerSession
            {
                AccountId = player.AccountId,
                Nickname = player.Nickname,
                LastBattleTime = player.LastBattleTime.DateTime,
                Battles = player.Battles,
                AvgTier = player.AvgTier ?? 0,
                WinRate = player.WinRate,
                AvgDamage = player.AvgDamage,
                AvgXp = player.AvgXp,
                DamageCoefficient = player.DamageCoefficient,
                SurvivalRate = player.SurvivalRate,
                Wn7 = player.Wn7
            };
        }

        public static PlayerTankSession ToTankSession(this TankInfoDto tankInfo, PlayerSession player)
        {
            return new PlayerTankSession
            {
                PlayerSession = player,
                AccountId = tankInfo.AccountId,
                TankId = tankInfo.TankId,
                LastBattleTime = tankInfo.LastBattleTime.DateTime,
                Battles = tankInfo.Battles,
                WinRate = tankInfo.WinRate,
                AvgDamage = tankInfo.AvgDamage,
                AvgXp = tankInfo.AvgXp,
                DamageCoefficient = tankInfo.DamageCoefficient,
                SurvivalRate = tankInfo.SurvivalRate,
                Wn7 = tankInfo.Wn7
            };
        }
    }
}