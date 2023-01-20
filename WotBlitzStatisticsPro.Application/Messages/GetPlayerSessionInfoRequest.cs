namespace WotBlitzStatisticsPro.Application.Messages
{
    public record GetPlayerSessionInfoRequest(long AccountId): IRequest<SessionInfoDto?>;
}