using MediatR;
using WotBlitzStatisticsPro.WebUi.Messages;

namespace WotBlitzStatisticsPro.WebUi.Services
{
    public class DialogService :
        INotificationHandler<OpenTankDetailsNotification>
    {
        public Task Handle(OpenTankDetailsNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Should open tank {notification.Tank.TankId}. But DialogService is not implemented yet");
            return Task.CompletedTask;
        }
    }
}