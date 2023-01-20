namespace WotBlitzStatisticsPro.Persistence.Messages
{
    public record GetLastTwoPlayerSessionsRequest(long AccountId): IRequest<PlayerSession[]?>;
}