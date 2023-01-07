namespace WotBlitzStatisticsPro.Persistence.Messages
{
    public record GetVehiclesByIdsRequest(long[]? TankIds): IRequest<DictionaryVehicle[]>;

}