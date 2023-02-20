namespace WotBlitzStatisticsPro.Persistence.Messages
{
    public record GetPlanningByAccountId(long AccountId): IRequest<List<ResourcePlanning>?>;
}