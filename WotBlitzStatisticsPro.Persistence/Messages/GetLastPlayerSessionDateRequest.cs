namespace WotBlitzStatisticsPro.Persistence.Messages
{
    public record GetLastPlayerSessionDateRequest(long AccountId): IRequest<DateTime?>;

}