namespace WotBlitzStatisticsPro.Messages
{
    public record GetPlayerInfoRequest(long accountId, string locale): IRequest<PlayerInfoDto>;
}