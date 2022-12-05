using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Messages;
using WotBlitzStatisticsPro.Application.Mocks;

namespace WotBlitzStatisticsPro.WebUi.Tests.PagesTests
{
    public class SearchPageTests: TestContextBase
    {
        private IRenderedComponent<SearchPlayer> _component;
        private List<ShortPlayerInfoDto> _playersExample;

        [SetUp]
        public async Task Setup()
        {
            var searchPlayersMock = new FindPlayersServiceMock();
            _playersExample = await searchPlayersMock.FindPlayers("any");
            MediatorMock.Setup(m => m.Send(It.IsAny<FindPlayersRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(_playersExample);
            _component = TestContext!.RenderComponent<SearchPlayer>();
        }

        [Test]
        public void ShouldFireMediatorEventWhenUserWantsToFindPlayer()
        {
            string searchString = "mboga";
            // TODO: find search text box, set up search string and click enter

            MediatorMock.Verify(m => m.Publish(
                It.Is<FindPlayersRequest>(n => n.SearchString == searchString),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void ShouldFireMediatorEventWhenUserWantsToOpenPlayerStatistics()
        {
            // TODO: find search text box, set up search string and click enter
            // TODO: find list-group-item array
            // TODO: Select SECOND of them and click a `btn` button

            // Second item
            long expectedAccountId = _playersExample[1].AccountId;
            MediatorMock.Verify(m => m.Publish(
                It.Is<NavigateToPlayerInfoPageNotification>(n => n.accountId == expectedAccountId),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}