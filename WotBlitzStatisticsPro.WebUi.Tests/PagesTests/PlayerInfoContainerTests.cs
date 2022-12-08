namespace WotBlitzStatisticsPro.WebUi.Tests.PagesTests
{
    public class PlayerInfoContainerTests: TestContextBase
    {
        private IRenderedComponent<PlayerInfoContainer> _component;
        private PlayerInfoDto _playerInfo;

        [SetUp]
        public async Task Setup()
        {
            var playerInfoMock = new PlayerInfoServiceMock();
            _playerInfo = await playerInfoMock.GetFullPlayerStatistics(1234, RequestLanguage.En);
            MediatorMock.Setup(m => m.Send(It.IsAny<GetPlayerInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(_playerInfo);
            _component = TestContext!.RenderComponent<PlayerInfoContainer>();
        }

        [Test]
        public void ShouldShowAppropriatePlayerInfoHeader()
        {
            var header = _component.Find("h1");
            header.TextContent.Should().Be(_playerInfo.Nickname);
        }
        
    }
}