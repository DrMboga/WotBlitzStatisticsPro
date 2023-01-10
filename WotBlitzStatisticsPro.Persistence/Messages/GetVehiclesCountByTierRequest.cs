namespace WotBlitzStatisticsPro.Persistence.Messages
{
    public record GetVehiclesCountByTierRequest(int Tier, bool? IsPremium): IRequest<int>;
}