namespace WotBlitzStatisticsPro.Application.Services
{
    public class ResourcePlanningRequestHandler
        : INotificationHandler<AddResourcePlanningNotification>
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
    }
}