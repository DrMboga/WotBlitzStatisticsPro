namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetStaticDictionariesRequest(RequestLanguage Language): IRequest<StaticDictionariesResponse>;
}