namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerSessionService : 
        INotificationHandler<SaveNewPlayerSessionNotification>,
        IRequestHandler<GetPlayerSessionInfoRequest, SessionInfoDto?>
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

        public async Task<SessionInfoDto?> Handle(GetPlayerSessionInfoRequest request, CancellationToken cancellationToken)
        {
            var lastTwoSessions = await _mediator.Send(new GetLastTwoPlayerSessionsRequest(request.AccountId));
            if(lastTwoSessions == null || lastTwoSessions.Length < 2)
            {
                return null;
            }
            // First row - last session, second row - previous session
            return new SessionInfoDto(
                lastTwoSessions[1].LastBattleTime,
                lastTwoSessions[0].Battles - lastTwoSessions[1].Battles,
                Convert.ToDecimal(lastTwoSessions[0].AvgTier - lastTwoSessions[1].AvgTier),
                lastTwoSessions[0].WinRate - lastTwoSessions[1].WinRate,
                lastTwoSessions[0].AvgDamage - lastTwoSessions[1].AvgDamage,
                lastTwoSessions[0].AvgXp - lastTwoSessions[1].AvgXp,
                lastTwoSessions[0].DamageCoefficient - lastTwoSessions[1].DamageCoefficient,
                lastTwoSessions[0].SurvivalRate - lastTwoSessions[1].SurvivalRate,
                Convert.ToDecimal(lastTwoSessions[0].Wn7 - lastTwoSessions[1].Wn7)
            );
        }
    }
}