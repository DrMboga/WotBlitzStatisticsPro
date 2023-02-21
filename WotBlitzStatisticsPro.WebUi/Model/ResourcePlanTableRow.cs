namespace WotBlitzStatisticsPro.WebUi.Model
{
    public record ResourcePlanTableRow(
        int Order,
        long TankId,
        string TankNation,
        int TankTier,
        string TankType,
        string TankName,
        string TankPreview,
        long TankPrice,
        long TankPriceWithEquipmentAndDiscount,
        long TotalNonDefaultModulesPriceXp,
        DateTime? Bought
    );
}