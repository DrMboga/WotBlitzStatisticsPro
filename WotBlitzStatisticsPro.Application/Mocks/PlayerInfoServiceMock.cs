using System.Text.Json;
using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Services;
using WotBlitzStatisticsPro.Model;

namespace WotBlitzStatisticsPro.Application.Mocks
{
    public class PlayerInfoServiceMock : IPlayerInfoService
    {
        public Task<PlayerInfoDto> GetFullPlayerStatistics(long accountId, RequestLanguage language)
        {
            return Task.FromResult(JsonSerializer.Deserialize<PlayerInfoDto>(glaFiGsr)!);
        }

        private static string glaFiGsr = """ {"AccountId":561478103,"Nickname":"GlafiGSR1989","CreatedAt":"2019-03-14T05:24:12+01:00","MaxFragsTankId":7281,"MaxXpTankId":8513,"AvgTier":null,"LastBattleTime":"2022-11-30T18:27:09+01:00","Battles":3716,"CapturePoints":417,"DamageDealt":8374486,"DamageReceived":4335553,"DroppedCapturePoints":3423,"Frags":5177,"Frags8P":2613,"Hits":37221,"Losses":1123,"MaxFrags":7,"MaxXp":2614,"Shots":42281,"Spotted":7236,"SurvivedBattles":1908,"WinAndSurvived":1889,"Wins":2560,"Xp":4088550}""";
    }
}