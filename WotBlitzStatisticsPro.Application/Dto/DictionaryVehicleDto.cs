namespace WotBlitzStatisticsPro.Application.Dto
{
    public record DictionaryVehicleDto(
        long TankId,
        short Tier,
        string Type,
        string Nation,
        bool IsPremium,
        long? PriceGold,
        long? PriceCredits,
        List<DictionaryNextVehicleDto>? nextTanks,
        int CurrentTankTreeRow,
        string Name,
        string? Description,
        string? PreviewImage,
        string? NormalImage,
        List<DictionaryVehicleModuleDto>? Modules
    );
}