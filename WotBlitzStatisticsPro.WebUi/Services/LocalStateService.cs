using System.Text.Json;
using Microsoft.JSInterop;
using WotBlitzStatisticsPro.WebUi.Model;

namespace WotBlitzStatisticsPro.WebUi.Services
{
    public class LocalStateService : ILocalStateService
    {
        private const string LocalStorageStateKey = "WotBlitzStatisticsPro";
        private readonly IJSRuntime _jsRuntime;

        public LocalStateService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<LocalState> ReadLocalState()
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
            return new LocalState(false, "en-US");
        }

        public async Task<LocalState> SetLocale(string locale)
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

        public async Task<LocalState> SetTheme(bool isDarkTheme)
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