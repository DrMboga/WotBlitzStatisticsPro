namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerSessionService : 
        INotificationHandler<SaveNewPlayerSessionNotification>,
        IRequestHandler<GetPlayerSessionInfoRequest, SessionInfoDto?>,
        IRequestHandler<GetTankSessionInfoRequest, SessionInfoDto?>,
        IRequestHandler<GetPlayerHistoryRequest, SessionHistoryItemDto[]>,
        IRequestHandler<GetTankHistoryRequest, SessionHistoryItemDto[]>
    {
        private readonly IMediator _mediator;

        public PlayerSessionService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(SaveNewPlayerSessionNotification notification, CancellationToken cancellationToken)
        {
            var lastSessionDate = await _mediator.Send(new GetLastPlayerSessionDateRequest(notification.Player.AccountId));

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

        public async Task<SessionInfoDto?> Handle(GetTankSessionInfoRequest request, CancellationToken cancellationToken)
        {
            var lastTwoSessions = await _mediator.Send(new GetLastTwoTankSessionRequest(request.AccountId, request.TankId));
            if(lastTwoSessions == null || lastTwoSessions.Length < 2)
            {
                return null;
            }
            // First row - last session, second row - previous session
            return new SessionInfoDto(
                lastTwoSessions[1].LastBattleTime,
                lastTwoSessions[0].Battles - lastTwoSessions[1].Battles,
                0,
                lastTwoSessions[0].WinRate - lastTwoSessions[1].WinRate,
                lastTwoSessions[0].AvgDamage - lastTwoSessions[1].AvgDamage,
                lastTwoSessions[0].AvgXp - lastTwoSessions[1].AvgXp,
                lastTwoSessions[0].DamageCoefficient - lastTwoSessions[1].DamageCoefficient,
                lastTwoSessions[0].SurvivalRate - lastTwoSessions[1].SurvivalRate,
                Convert.ToDecimal(lastTwoSessions[0].Wn7 - lastTwoSessions[1].Wn7)
            );
        }

        public async Task<SessionHistoryItemDto[]> Handle(GetPlayerHistoryRequest request, CancellationToken cancellationToken)
        {
            var result = new List<SessionHistoryItemDto>();
            var playerDbHistory = await _mediator.Send(new GetPlayerHistoryDbRequest(request.AccountId, 5));

            if (playerDbHistory != null)
            {
                foreach (var player in playerDbHistory)
                {
                    result.Add(player.ToSessionHistoryItem());
                }
            }

            return [..result];
        }

        public async Task<SessionHistoryItemDto[]> Handle(GetTankHistoryRequest request, CancellationToken cancellationToken)
        {
            var result = new List<SessionHistoryItemDto>();

            var tankHistory = await _mediator.Send(new GetTankHistoryDbRequest(request.AccountId, request.TankId, 5));
            if (tankHistory != null)
            {
                foreach (var tank in tankHistory)
                {
                    result.Add(tank.ToSessionHistoryItem());
                }
            }            

            return [..result];
        }
    }
}