using MediatR;
using WotBlitzStatisticsPro.Model;
using WotBlitzStatisticsPro.WargamingApi.Model;

namespace WotBlitzStatisticsPro.WargamingApi.Messages
{
    public record GetAccountStatisticsRequest(long AccountId, RequestLanguage Language): IRequest<WotAccountInfo>;
}