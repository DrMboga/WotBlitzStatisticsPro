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
        public void ShouldFireMediatorEventWhenThemeChanged()
        {
            // It is light theme by default
            _component.Instance.IsDarkTheme.Should().Be(false);

            var themeIcon = _component.Find("img");

            themeIcon.Should().NotBeNull();
            themeIcon.Click();
            // Should switch theme
            _component.Instance.IsDarkTheme.Should().Be(true);
            MediatorMock.Verify(m => m.Publish(
                It.Is<SwitchThemeNotification>(n => n.IsDarkTheme == true),
                It.IsAny<CancellationToken>()), Times.Once);
            MediatorMock.Verify(m => m.Publish(
                It.Is<SaveThemeNotification>(n => n.IsDarkTheme == true),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestCase(0, "en-US")]
        [TestCase(1, "ru-RU")]
        [TestCase(2, "de-DE")]
        public void ShouldFireMediatorEventEachTimeWhenLanguageChanged(int dropdownButtonIndex, string expectedLanguageParameter)
        {
            // var html = _component.Markup;
            // Console.WriteLine(html);
            var elements = _component.FindAll(".dropdown-item");

            elements.Should().NotBeNull();
            elements.Count.Should().Be(3);

            elements[dropdownButtonIndex].Click();
            MediatorMock.Verify(m => m.Publish(
                It.Is<ChangeCultureNotification>(n => n.Culture == expectedLanguageParameter),
                It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}