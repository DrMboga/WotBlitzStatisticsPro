namespace WotBlitzStatisticsPro.Application.Mappers
{
    public static class TanksProfile
    {
        public static TankInfoDto[] ToTankInfoDto(this WotAccountTanksStatistics[]? tanks, DictionaryVehicle[]? dictionaryVehicles, Dictionary<long, AchievementsDto> achievements)
        {
            var result = new List<TankInfoDto>();

            if(tanks != null && tanks.Length > 0)            
            {
                foreach (var tank in tanks)
                {
                    var dictionaryVehicle = dictionaryVehicles?.FirstOrDefault(v => v.TankId == tank.TankId);
                    var tankDto = tank.ToTankDto(dictionaryVehicle);
                    if(achievements != null && achievements.ContainsKey(tank.TankId))
                    {
                        tankDto.Achievements = achievements[tank.TankId];
                    }
                    result.Add(tankDto);
                }
            }
            return result.ToArray();
        }

        private static TankInfoDto ToTankDto(this WotAccountTanksStatistics tank, DictionaryVehicle? dictionaryVehicle)
        {
            var tankInfo = new TankInfoDto 
            {
                AccountId = tank.AccountId,
                TankId = tank.TankId,
                InGarage = tank.InGarage,
                MarkOfMastery = tank.MarkOfMastery.ToMastery(),
                BattleLifeTimeInSeconds = tank.BattleLifeTimeInSeconds,
                Name = dictionaryVehicle?.Name,
                TankNation = dictionaryVehicle?.Nation,
                Tier = dictionaryVehicle?.Tier ?? 1,
                TankType = dictionaryVehicle?.Type,
                IsPremium = dictionaryVehicle?.IsPremium ?? false,
                PreviewImage = dictionaryVehicle?.PreviewImage,
                NormalImage = dictionaryVehicle?.Image,
                LastBattleTime = tank.LastBattleTime.ToDateTime(),
                Battles = tank.All?.Battles ?? 0,
                CapturePoints = tank.All?.CapturePoints ?? 0,
                DamageDealt = tank.All?.DamageDealt ?? 0,
                DamageReceived = tank.All?.DamageReceived ?? 0,
                DroppedCapturePoints = tank.All?.DroppedCapturePoints ?? 0,
                Frags = tank.All?.Frags ?? 0,
                Frags8P = tank.All?.Frags8P ?? 0,
                Hits = tank.All?.Hits ?? 0,
                Losses = tank.All?.Losses ?? 0,
                MaxFrags = tank.All?.MaxFrags ?? 0,
                MaxXp = tank.All?.MaxXp ?? 0,
                Shots = tank.All?.Shots ?? 0,
                Spotted = tank.All?.Spotted ?? 0,
                SurvivedBattles = tank.All?.SurvivedBattles ?? 0,
                WinAndSurvived = tank.All?.WinAndSurvived ?? 0,
                Wins = tank.All?.Wins ?? 0,
                Xp = tank.All?.Xp ?? 0
            };

            tankInfo.CalculateWn7();

            return tankInfo;
        }

        private static MarkOfMastery ToMastery(this long mastery)
        {
            switch(mastery)
            {
                case 4:
                    return MarkOfMastery.Master;
                case 3:
                    return MarkOfMastery.Rank1;
                case 2:
                    return MarkOfMastery.Rank2;
                case 1:
                    return MarkOfMastery.Rank3;
                default:
                    return MarkOfMastery.None;
            }
        }
    }
}