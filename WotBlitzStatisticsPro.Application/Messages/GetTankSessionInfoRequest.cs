namespace WotBlitzStatisticsPro.Application.Messages
{
    public record GetTankSessionInfoRequest(long AccountId, long TankId): IRequest<SessionInfoDto?>;
}