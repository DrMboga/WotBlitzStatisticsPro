namespace WotBlitzStatisticsPro.Application.Mappers
{
    // TODO: AutoMapper makes it better. But AutoMapper somehow works improperly works with blazor wasm under the the linux container...
    public static class AccountInfoProfile
    {
        public static PlayerInfoDto MapToPlayerInfoDto(this WotAccountInfo wotAccountInfo){
            return new PlayerInfoDto {
                AccountId = wotAccountInfo.AccountId,
                Nickname = wotAccountInfo.Nickname ?? string.Empty,
                CreatedAt = wotAccountInfo.CreatedAt.ToDateTime(),
                MaxFragsTankId = wotAccountInfo?.Statistics?.All?.MaxFragsTankId ?? 0,
                MaxXpTankId = wotAccountInfo?.Statistics?.All?.MaxXpTankId ?? 0,
                LastBattleTime = wotAccountInfo?.LastBattleTime.ToDateTime() ?? new DateTime(1970, 1, 1),
                Battles = wotAccountInfo?.Statistics?.All?.Battles ?? 0,
                CapturePoints = wotAccountInfo?.Statistics?.All?.CapturePoints ?? 0,
                DamageDealt = wotAccountInfo?.Statistics?.All?.DamageDealt ?? 0,
                DamageReceived = wotAccountInfo?.Statistics?.All?.DamageReceived ?? 0,
                DroppedCapturePoints = wotAccountInfo?.Statistics?.All?.DroppedCapturePoints ?? 0,
                Frags = wotAccountInfo?.Statistics?.All?.Frags ?? 0,
                Frags8P = wotAccountInfo?.Statistics?.All?.Frags8P ?? 0,
                Hits = wotAccountInfo?.Statistics?.All?.Hits ?? 0,
                Losses = wotAccountInfo?.Statistics?.All?.Losses ?? 0,
                MaxFrags = wotAccountInfo?.Statistics?.All?.MaxFrags ?? 0,
                MaxXp = wotAccountInfo?.Statistics?.All?.MaxXp ?? 0,
                Shots = wotAccountInfo?.Statistics?.All?.Shots ?? 0,
                Spotted = wotAccountInfo?.Statistics?.All?.Spotted ?? 0,
                SurvivedBattles = wotAccountInfo?.Statistics?.All?.SurvivedBattles ?? 0,
                WinAndSurvived = wotAccountInfo?.Statistics?.All?.WinAndSurvived ?? 0,
                Wins = wotAccountInfo?.Statistics?.All?.Wins ?? 0,
                Xp = wotAccountInfo?.Statistics?.All?.Xp ?? 0
            };
        }
    }
}