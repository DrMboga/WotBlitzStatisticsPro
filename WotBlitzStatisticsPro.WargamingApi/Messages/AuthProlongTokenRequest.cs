namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record AuthProlongTokenRequest(string AuthToken): IRequest<WotAuthProlongateResponse>;
}