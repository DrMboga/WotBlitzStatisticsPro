namespace WotBlitzStatisticsPro.Application.Services
{
    public class ResourcePlanningRequestHandler
        : INotificationHandler<AddResourcePlanningNotification>,
        IRequestHandler<GetResourcePlansRequest, ResourcePlanDto[]?>,
        INotificationHandler<MarkPlanTankAsBoughtNotification>
    {
        private readonly IMediator _mediator;

        public ResourcePlanningRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(AddResourcePlanningNotification notification, CancellationToken cancellationToken)
        {
            var state = await _mediator.Send(new ReadStateRequest());
            if (state?.LoggedInAccountId == null)
            {
                return;
            }
            var allPlans = await _mediator.Send(new GetPlanningByAccountId(state.LoggedInAccountId.Value));
            if (allPlans == null || allPlans.Any(p => p.TankId == notification.TankId))
            {
                return;
            }
            int maxOrder = allPlans.Count == 0 ? 0 : allPlans.Max(p => p.Order);

            await _mediator.Publish(new AddNewPlanningNotification(
                state.LoggedInAccountId.Value, 
                notification.TankId, 
                maxOrder + 1, 
                notification.SaleCert, 
                notification.PlanningEquipment));
        }

        public async Task<ResourcePlanDto[]?> Handle(GetResourcePlansRequest request, CancellationToken cancellationToken)
        {
            var allPlans = await _mediator.Send(new GetPlanningByAccountId(request.AccountId));
            return allPlans.ToResourcePlanDto();
        }

        public Task Handle(MarkPlanTankAsBoughtNotification notification, CancellationToken cancellationToken)
        {
            return _mediator.Publish(new UpdatePlanBoughtMarkNotification(notification.AccountId, notification.TankId, DateTime.Now));
        }
    }
}