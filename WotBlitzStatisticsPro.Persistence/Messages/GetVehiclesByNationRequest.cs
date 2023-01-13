namespace WotBlitzStatisticsPro.Persistence.Messages
{
    public record GetVehiclesByNationRequest(string Nation): IRequest<DictionaryVehicle[]>;
}