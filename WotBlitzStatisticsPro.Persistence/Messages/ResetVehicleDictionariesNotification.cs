namespace WotBlitzStatisticsPro.Persistence.Messages
{
    public record ResetVehicleDictionariesNotification(List<DictionaryVehicle> Vehicles): INotification;
}