using MediatR;
using Microsoft.JSInterop;
using WotBlitzStatisticsPro.WebUi.Messages;

namespace WotBlitzStatisticsPro.WebUi.Services
{
    public class MediaQueriesService : IRequestHandler<IsScreenLessThenValueRequest, bool>
    {
        private readonly IJSRuntime _jsRuntime;

        public MediaQueriesService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> Handle(IsScreenLessThenValueRequest request, CancellationToken cancellationToken)
        {
            var isLess = await _jsRuntime.InvokeAsync<bool>("mediaQueries.IsScreenWidthLessThen", request.WidthInPixels);
            return isLess;
        }
    }
}