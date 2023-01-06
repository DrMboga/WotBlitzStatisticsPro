namespace WotBlitzStatisticsPro.Application.Dto
{
    public record DictionaryVehicleModuleDto(
        long ModuleId,
        string Name,
        string Type,
        long PriceXp,
        bool IsDefault
    );
}