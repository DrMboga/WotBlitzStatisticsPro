using System.Text.Json;

namespace WotBlitzStatisticsPro.Application.Mocks
{
    public class FindPlayersServiceMock: IFindPlayersService
    {
        public Task<List<ShortPlayerInfoDto>> FindPlayers(string searchString)
        {
            return Task.FromResult(JsonSerializer.Deserialize<List<ShortPlayerInfoDto>>(JsonFindGlafiResponse)!);
        }

        private static string JsonFindGlafiResponse = """[{"AccountId":566680444,"Nickname":"GLAFI_Sotva","CreatedAt":"1970-01-01T01:00:00+01:00","Battles":858,"LastBattle":"2020-08-18T16:55:56+02:00","MaxFragsTankId":null,"MaxXpTankId":null,"ClanTag":"TPZO","WinRate":45,"AvgTier":null},{"AccountId":536032857,"Nickname":"GLAFI_TOP_CLAN","CreatedAt":"1970-01-01T01:00:00+01:00","Battles":11,"LastBattle":"2016-03-04T22:15:48+01:00","MaxFragsTankId":null,"MaxXpTankId":null,"ClanTag":null,"WinRate":72,"AvgTier":null},{"AccountId":547645810,"Nickname":"Glafi_UA","CreatedAt":"1970-01-01T01:00:00+01:00","Battles":3,"LastBattle":"2020-08-18T00:51:37+02:00","MaxFragsTankId":null,"MaxXpTankId":null,"ClanTag":null,"WinRate":100,"AvgTier":null},{"AccountId":548084720,"Nickname":"Glafi_UA_2017","CreatedAt":"1970-01-01T01:00:00+01:00","Battles":3,"LastBattle":"2017-06-12T01:37:54+02:00","MaxFragsTankId":null,"MaxXpTankId":null,"ClanTag":null,"WinRate":66,"AvgTier":null},{"AccountId":567963776,"Nickname":"glafi_winner","CreatedAt":"1970-01-01T01:00:00+01:00","Battles":53,"LastBattle":"2020-02-27T18:28:37+01:00","MaxFragsTankId":null,"MaxXpTankId":null,"ClanTag":null,"WinRate":66,"AvgTier":null},{"AccountId":588016249,"Nickname":"GLAFIATOR_se","CreatedAt":"1970-01-01T01:00:00+01:00","Battles":1,"LastBattle":"2022-10-31T18:27:55+01:00","MaxFragsTankId":null,"MaxXpTankId":null,"ClanTag":null,"WinRate":100,"AvgTier":null},{"AccountId":578112504,"Nickname":"Glaficom324","CreatedAt":"1970-01-01T01:00:00+01:00","Battles":6076,"LastBattle":"2022-11-26T14:28:35+01:00","MaxFragsTankId":null,"MaxXpTankId":null,"ClanTag":"DVFO","WinRate":57,"AvgTier":null},{"AccountId":563905924,"Nickname":"Glafidatcam","CreatedAt":"1970-01-01T01:00:00+01:00","Battles":3,"LastBattle":"2019-07-23T20:07:07+02:00","MaxFragsTankId":null,"MaxXpTankId":null,"ClanTag":null,"WinRate":100,"AvgTier":null},{"AccountId":571781760,"Nickname":"glafidatcom","CreatedAt":"1970-01-01T01:00:00+01:00","Battles":494,"LastBattle":"2020-09-30T11:10:07+02:00","MaxFragsTankId":null,"MaxXpTankId":null,"ClanTag":null,"WinRate":65,"AvgTier":null},{"AccountId":568130825,"Nickname":"GlafiFan777","CreatedAt":"1970-01-01T01:00:00+01:00","Battles":198,"LastBattle":"2022-10-14T20:05:36+02:00","MaxFragsTankId":null,"MaxXpTankId":null,"ClanTag":null,"WinRate":68,"AvgTier":null},{"AccountId":561478103,"Nickname":"GlafiGSR1989","CreatedAt":"1970-01-01T01:00:00+01:00","Battles":3619,"LastBattle":"2022-11-26T23:14:36+01:00","MaxFragsTankId":null,"MaxXpTankId":null,"ClanTag":"G_T_M","WinRate":68,"AvgTier":null}]""";
    }
}