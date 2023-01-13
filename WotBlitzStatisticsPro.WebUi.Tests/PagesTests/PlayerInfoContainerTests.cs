using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Messages;
using WotBlitzStatisticsPro.Application.Mocks;
using WotBlitzStatisticsPro.Model;
using WotBlitzStatisticsPro.WebUi.Services;

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
            TestContext.Services.AddTransient<ISvgHelper, SvgHelper>();
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