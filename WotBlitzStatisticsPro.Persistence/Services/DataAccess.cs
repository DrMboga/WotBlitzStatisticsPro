namespace WotBlitzStatisticsPro.Persistence.Services
{
    public class DataAccess :
        INotificationHandler<ResetVehicleDictionariesNotification>,
        INotificationHandler<UpdateStateNotification>
    {
        // TODO: Add dbContext

        public Task Handle(ResetVehicleDictionariesNotification notification, CancellationToken cancellationToken)
        {
            // Clear vehicles, NextVehicle and VehicleModule tables
            // Insert new vehicles dictionary
            Console.WriteLine("Vehicle dictionaries update handler");
            return Task.CompletedTask;
        }

        public Task Handle(UpdateStateNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("State update handler");
            return Task.CompletedTask;
        }
    }
}