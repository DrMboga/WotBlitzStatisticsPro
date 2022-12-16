namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetDictionaryVehicleModules(RequestLanguage Language): IRequest<WotEncyclopediaVehicleModulesResponse>;
}