namespace WotBlitzStatisticsPro.Application.Messages
{
    public record GetResourcePlansRequest(long AccountId): IRequest<ResourcePlanDto[]?>;
}