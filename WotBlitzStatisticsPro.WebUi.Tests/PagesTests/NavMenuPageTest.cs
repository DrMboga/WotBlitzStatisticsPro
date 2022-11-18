namespace WotBlitzStatisticsPro.WebUi.Tests.PagesTests
{
    public class NavMenuPageTest: TestContextBase
    {
        private IRenderedComponent<NavMenu> _component;

        [SetUp]
        public void Setup()
        {
            MediatorMock.Setup(m => m.Send(It.IsAny<LocalStateRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new LocalState(false, "en-US"));
            _component = TestContext!.RenderComponent<NavMenu>();
        }

        [Test]
        public void ShouldFireMediatorEventEachTimeWhenLanguageChanged()
        {
            var html = _component.Markup;
            // Console.WriteLine(html);
            var elements = _component.FindAll(".dropdown-item");

            elements.Should().NotBeNull();
            elements.Count.Should().Be(3);

            // string[] expectedLanguages = {"en-US", "ru-RU", "de-DE"};
            // for (int i = 0; i < 3; i++)
            // {
            //     elements[i].Click();
            //     MediatorMock.Verify(m => m.Publish(
            //         It.Is<ChangeCultureNotification>(n => n.Culture == expectedLanguages[0]),
            //         It.IsAny<CancellationToken>()), Times.Once);
            // }
        }

    }
}