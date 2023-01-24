using MediatR;
using Microsoft.JSInterop;
using WotBlitzStatisticsPro.WebUi.Messages;

namespace WotBlitzStatisticsPro.WebUi.Services
{
    public class DialogService :
        INotificationHandler<OpenTankDetailsNotification>
    {
        private readonly IJSRuntime _jsRuntime;

        public DialogService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task Handle(OpenTankDetailsNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Should open tank {notification.Tank.TankId}. But DialogService is not implemented yet");
            await _jsRuntime.InvokeVoidAsync("modal.show", notification.Tank);
        }
    }
}