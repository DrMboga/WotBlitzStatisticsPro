namespace WotBlitzStatisticsPro.Application.Tests
{
    public class PlayerInfoResponseTests
    {
        private Mock<IFindPlayersService> _findPlayerServiceMock;
        private Mock<IPlayerInfoService> _playerInfoServiceMock;
        private Mock<IClanInfoService> _clanInfoInfoServiceMock;
        private PlayerRequestsHandler _service;

        [SetUp]
        protected void Setup()
        {
            _findPlayerServiceMock = new();
            _playerInfoServiceMock = new();
            _clanInfoInfoServiceMock = new();
            _service = new PlayerRequestsHandler(_findPlayerServiceMock.Object, _playerInfoServiceMock.Object, _clanInfoInfoServiceMock.Object) ;
        }

        [Test]
        public async Task ShouldCallFindPlayersOnMediatREvent()
        {
            var searchString = "mboga";
            var players = await _service.Handle(new FindPlayersRequest(searchString), CancellationToken.None);

            _findPlayerServiceMock.Verify(s => s.FindPlayers(searchString), Times.Once);
        }

        public async Task ShouldCallGetPlayerInfoOnMediatREvent()
        {
            long accountId = 123456789;
            string locale = "en-US";

            var account = await _service.Handle(new GetPlayerInfoRequest(accountId, locale), CancellationToken.None);

            _playerInfoServiceMock.Verify(s => s.GetFullPlayerStatistics(accountId, RequestLanguage.En));
        }
    }
}