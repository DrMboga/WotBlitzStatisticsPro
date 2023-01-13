namespace WotBlitzStatisticsPro.Application.Messages
{
    public record GetDictionaryVehiclesByNationRequest(string Nation): IRequest<DictionaryVehicleDto[]>;
}