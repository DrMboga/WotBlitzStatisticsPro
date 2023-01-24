namespace WotBlitzStatisticsPro.Persistence.Messages
{
    public record GetLastTwoTankSessionRequest(long AccountId, long TankId): IRequest<PlayerTankSession[]?>;
}