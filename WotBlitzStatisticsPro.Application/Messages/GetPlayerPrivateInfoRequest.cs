namespace WotBlitzStatisticsPro.Application.Messages
{
    public record GetPlayerPrivateInfoRequest(long AccountId, string Locale, string AccessToken): IRequest<PlayerPrivateInfoDto>;
}