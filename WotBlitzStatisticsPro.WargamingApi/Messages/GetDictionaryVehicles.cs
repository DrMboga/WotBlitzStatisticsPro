namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetDictionaryVehicles(RequestLanguage Language): IRequest<List<WotEncyclopediaVehiclesResponse>>;
}