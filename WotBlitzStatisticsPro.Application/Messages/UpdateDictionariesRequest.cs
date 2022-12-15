namespace WotBlitzStatisticsPro.Application.Messages
{
    public record UpdateDictionariesRequest(string locale): IRequest<DictionariesInfoDto>;
}