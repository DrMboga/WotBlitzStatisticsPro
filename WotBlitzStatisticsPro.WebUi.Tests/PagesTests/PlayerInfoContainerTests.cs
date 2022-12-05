using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Messages;
using WotBlitzStatisticsPro.Application.Mocks;
using WotBlitzStatisticsPro.Model;

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
        public void ShouldShowAppropriatePlayerInfo()
        {
            // TODO: find h1 tag and check if it is reflects the _playerInfo.Nickname property
            Assert.Fail("find h1 tag and check if it is reflects the _playerInfo.Nickname property");
        }
        
    }
}