namespace WotBlitzStatisticsPro.Application.Tests
{
    public class FindPlayersServiceTests
    {
        private Mock<IMediator> _mediatorMock;
        private FindPlayersService _service;

        [SetUp]
        protected void Setup()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AccountSearchResponseProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            
            _mediatorMock = new();
            _service = new FindPlayersService(_mediatorMock.Object, mapper);
        }

        [Test]
        public async Task ShouldCallAppropriateMethodsOnFindPlayers()
        {
            var listsResponse = new List<WotAccountListResponse> {
                new WotAccountListResponse { AccountId = 1234, Nickname = "First"},
                new WotAccountListResponse { AccountId = 12345, Nickname = "Second"},
            };

            var listAccountInfos = new List<WotAccountInfo> {
                new WotAccountInfo { AccountId = 1234, Nickname = "First", LastBattleTime = 1669892117, 
                    Statistics = new WotAccountStatistics {
                        All = new WotAccountFullStatistics {
                            Battles = 1245
                        }
                    }},
                new WotAccountInfo { AccountId = 12345, Nickname = "Second", LastBattleTime = 1670079317, 
                    Statistics = new WotAccountStatistics {
                        All = new WotAccountFullStatistics {
                            Battles = 4227
                        }
                    }},
            };

            var clanAccountInfo = new List<ClanAccountInfo> {
                new ClanAccountInfo { AccountId = 12345, AccountName = "Second", ClanId = 7654321 },
            };

            var clansInfo = new List<ClanInfo> {
                new ClanInfo { ClanId = 7654321, Tag = "XXXY"}
            };

            var searchString = "mboga";
            _mediatorMock.Setup(m => m.Send(It.Is<GetAccountsListRequest>(r => r.SearchString == searchString), It.IsAny<CancellationToken>()))
                .ReturnsAsync(listsResponse);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBunchOfAccountsRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(listAccountInfos);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBulkClanAccountInfosRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(clanAccountInfo);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetClanInfoRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(clansInfo);

            var players = await _service.FindPlayers(searchString);

            players.Should().NotBeNull();
            players.Count.Should().Be(listsResponse.Count);

            players[0].AccountId.Should().Be(listsResponse[0].AccountId);
            players[1].AccountId.Should().Be(listsResponse[1].AccountId);
            players[0].Nickname.Should().Be(listsResponse[0].Nickname);
            players[1].Nickname.Should().Be(listsResponse[1].Nickname);
            var expectedLastBattleFirst = listAccountInfos[0].LastBattleTime.ToDateTime();
            var expectedLastBattleSecond = listAccountInfos[1].LastBattleTime.ToDateTime();
            players[0].LastBattle.Should().Be(expectedLastBattleFirst);
            players[1].LastBattle.Should().Be(expectedLastBattleSecond);
            players[0].Battles.Should().Be(1245);
            players[1].Battles.Should().Be(4227);
            players[0].ClanTag.Should().BeNull();
            players[1].ClanTag.Should().Be(clansInfo[0].Tag);
        }
    }
}