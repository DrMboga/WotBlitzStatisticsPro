using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WotBlitzStatisticsPro.WebUi.Messages;
using WotBlitzStatisticsPro.WebUi.Model;

namespace WotBlitzStatisticsPro.WebUi.Services
{
    public class LocalStateService: 
        IRequestHandler<LocalStateRequest, LocalState>,
        INotificationHandler<SwitchThemeNotification>,
        INotificationHandler<SaveThemeNotification>,
        INotificationHandler<ChangeCultureNotification>
    {
        private const string LocalStorageStateKey = "WotBlitzStatisticsPro";
        private readonly IJSRuntime _jsRuntime;
        private readonly IMediator _mediator;
        private readonly NavigationManager _navigationManager;


        public LocalStateService(IJSRuntime jsRuntime, IMediator mediator, NavigationManager navigationManager)
        {
            _jsRuntime = jsRuntime;
            _mediator = mediator;
            _navigationManager = navigationManager;
        }

        public Task<LocalState> Handle(LocalStateRequest request, CancellationToken cancellationToken)
        {
            return ReadLocalState();
        }

        public async Task Handle(SwitchThemeNotification notification, CancellationToken cancellationToken)
        {
            await _jsRuntime.InvokeVoidAsync("themesHelper.switchTheme", notification.IsDarkTheme);
        }

        public Task Handle(SaveThemeNotification notification, CancellationToken cancellationToken)
        {
            return SetTheme(notification.IsDarkTheme);
        }

        public async Task Handle(ChangeCultureNotification notification, CancellationToken cancellationToken)
        {
            await SetLocale(notification.Culture);
            _navigationManager.NavigateTo(_navigationManager.Uri, forceLoad: true);
        }

        private async Task<LocalState> ReadLocalState()
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", LocalStorageStateKey);
            if(!string.IsNullOrEmpty(json))
            {
                var state = JsonSerializer.Deserialize<LocalState>(json);
                if(state != null)
                {
                    return state;
                }
            }
            return new LocalState(true, "en-US");
        }

        private async Task<LocalState> SetLocale(string locale)
        {
            var state = await ReadLocalState();
            if (state.Locale != locale)
            {
                var newState = new LocalState(state.IsDarkTheme, locale);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem",
                LocalStorageStateKey, JsonSerializer.Serialize(newState));
                return newState;
            }
            return state;
        }

        private async Task<LocalState> SetTheme(bool isDarkTheme)
        {
            var state = await ReadLocalState();
            if (state.IsDarkTheme != isDarkTheme)
            {
                var newState = new LocalState(isDarkTheme, state.Locale);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem",
                LocalStorageStateKey, JsonSerializer.Serialize(newState));
                return newState;
            }
            return state;
        }
    }
}