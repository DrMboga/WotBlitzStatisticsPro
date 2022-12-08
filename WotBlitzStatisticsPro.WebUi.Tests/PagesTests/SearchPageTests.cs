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
            var input = _component.Find("input");
            input.Change(searchString);

            MediatorMock.Verify(m => m.Send(
                It.Is<FindPlayersRequest>(n => n.SearchString == searchString),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldFireMediatorEventWhenUserWantsToOpenPlayerStatistics()
        {
            // Triggering the list box filling
            string searchString = "mboga";
            var input = _component.Find("input");
            await input.ChangeAsync(new ChangeEventArgs {Value = searchString});

            // Finding the list elements array
            var listItems = _component.FindAll(".list-group-item");
            listItems.Should().NotBeNull();
            listItems.Count.Should().BeGreaterThan(1);
            listItems[1].Should().NotBeNull();

            // Selecting the second item
            listItems[1].Click();

            // Find a button that should appear in the second item
            var button = _component.Find("button");
            button.Should().NotBeNull();
            button.Click();

            // Second item in the list
            long expectedAccountId = _playersExample[1].AccountId;
            MediatorMock.Verify(m => m.Publish(
                It.Is<NavigateToPlayerInfoPageNotification>(n => n.accountId == expectedAccountId),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}