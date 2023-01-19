namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerSessionService : INotificationHandler<SaveNewPlayerSessionNotification>
    {
        private readonly IMediator _mediator;

        public PlayerSessionService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(SaveNewPlayerSessionNotification notification, CancellationToken cancellationToken)
        {
            var lastSessionDate = await _mediator.Send(new GetLastPlayerSessionDateRequest(notification.Player.AccountId));

            Console.WriteLine($"Player session LastBattle {(lastSessionDate == null ? "null" : lastSessionDate.Value.ToString("d"))}; Current LastBattle {notification.Player.LastBattleTime.Date}");

            if (lastSessionDate.HasValue && lastSessionDate.Value.Date == notification.Player.LastBattleTime.Date)
            {
                return;
            }

            var playerSession = notification.Player.ToPlayerSession();
            var tanksSession = notification?
                                .Player?
                                .Tanks?
                                .Where(t => t.LastBattleTime.DateTime > (lastSessionDate ?? new DateTime(1,1,1)))
                                .Select(t => t.ToTankSession(playerSession))
                                .ToList();
            playerSession.TanksSession = tanksSession;

            Console.WriteLine($"Tanks from last session {tanksSession?.Count}");

            await _mediator.Publish(new InsertNewPlayersSessionNotification(playerSession));
        }
    }
}