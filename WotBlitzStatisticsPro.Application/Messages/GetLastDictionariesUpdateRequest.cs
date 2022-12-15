namespace WotBlitzStatisticsPro.Application.Messages
{
    public record GetLastDictionariesUpdateRequest(string locale): IRequest<DictionariesInfoDto>;
}