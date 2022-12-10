namespace WotBlitzStatisticsPro.Tests.UiTests.ServiceTests
{
        public class LocalStateServiceTest: TestContextBase
    {
        private const string LocalStorageStateKey = "WotBlitzStatisticsPro";
        private LocalStateService _service;
        private BunitJSInterop _jsRuntimeMock;

        private LocalState _localState;

        [SetUp]
        public void Setup()
        {
            _localState = new LocalState(false, "en-US");
            _jsRuntimeMock = TestContext!.JSInterop;

            string localStorage = JsonSerializer.Serialize(_localState);
            _jsRuntimeMock.Setup<string>("localStorage.getItem", LocalStorageStateKey).SetResult(localStorage);

            _service = new LocalStateService(_jsRuntimeMock.JSRuntime, MediatorMock.Object, NavigationManagerMock);
        }

        [Test]
        public async Task ShouldReadLocalStateOnMediatorRequest()
        {
            var expectedState = await _service.Handle(new LocalStateRequest(), CancellationToken.None);
            expectedState.Should().NotBeNull();
            expectedState.IsDarkTheme.Should().Be(_localState.IsDarkTheme);
            expectedState.Locale.Should().Be(_localState.Locale);
        }

        [Test]
        public async Task ShouldCallJsChangeThemeMethodOnMediatorNotification()
        {
            var changeThemeRequest = new SwitchThemeNotification(true);
            var localStorageSetItemHandler = _jsRuntimeMock.SetupVoid("themesHelper.switchTheme", true);
            localStorageSetItemHandler.SetVoidResult();

            await _service.Handle(changeThemeRequest, CancellationToken.None);

            _jsRuntimeMock.Invocations.Count.Should().Be(1);
        }

        [Test]
        public async Task ShouldCallJsSaveToLocalStorageOnSaveThemeMediatorNotification()
        {
            var saveThemeNotification = new SaveThemeNotification(true);
            var expectedState = new LocalState(true, _localState.Locale);
            var expectedSerializedState = JsonSerializer.Serialize(expectedState);
            var localStorageSetItemHandler = _jsRuntimeMock.SetupVoid("localStorage.setItem", LocalStorageStateKey, expectedSerializedState);
            localStorageSetItemHandler.SetVoidResult();

            await _service.Handle(saveThemeNotification, CancellationToken.None);

            _jsRuntimeMock.Invocations.Count.Should().Be(2);
            var jsInvocations = _jsRuntimeMock.Invocations.ToArray();
            jsInvocations[0].Identifier.Should().Be("localStorage.getItem");
            jsInvocations[1].Identifier.Should().Be("localStorage.setItem");
        }

        [Test]
        public async Task ShouldCallJsSaveToLocalStorageOnChangeCultureMediatorNotification()
        {
            var changeCultureNotification = new ChangeCultureNotification("de-DE");
            var expectedState = new LocalState(_localState.IsDarkTheme, "de-DE");
            var expectedSerializedState = JsonSerializer.Serialize(expectedState);
            var localStorageSetItemHandler = _jsRuntimeMock.SetupVoid("localStorage.setItem", LocalStorageStateKey, expectedSerializedState);
            localStorageSetItemHandler.SetVoidResult();

            await _service.Handle(changeCultureNotification, CancellationToken.None);

            _jsRuntimeMock.Invocations.Count.Should().Be(2);
            var jsInvocations = _jsRuntimeMock.Invocations.ToArray();
            jsInvocations[0].Identifier.Should().Be("localStorage.getItem");
            jsInvocations[1].Identifier.Should().Be("localStorage.setItem");
        }
    }
}