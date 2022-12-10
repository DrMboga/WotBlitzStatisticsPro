namespace WotBlitzStatisticsPro.Tests.UiTests
{
    public class TestContextBase: TestContextWrapper
    {
        protected Mock<IMediator> MediatorMock;
        protected MockNavigationManager NavigationManagerMock;

        [SetUp]
        protected void Init()
        {
            MediatorMock = new();
            NavigationManagerMock = new();

            TestContext = new Bunit.TestContext();
            TestContext.Services.AddSingleton(MediatorMock.Object);
            TestContext.Services.AddLocalization();

            TestContext.Services.AddSingleton<NavigationManager>(NavigationManagerMock);
        }

        [TearDown]
        public void TearDown() => TestContext?.Dispose();
    }

}