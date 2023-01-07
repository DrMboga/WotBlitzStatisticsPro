namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetTankStatisticsRequest(long AccountId, RequestLanguage Language): IRequest<WotAccountTanksStatistics[]>;
}